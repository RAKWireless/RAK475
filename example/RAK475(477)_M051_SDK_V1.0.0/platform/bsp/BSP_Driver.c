/**
* @file 		BSP_Driver.c
* @author		harry
* @date			2016/6/1
* @version	1.0.0
* @par Copyright (c):
* 					rakwireless
* @par History:
*	version: author, date, desc\n
*/

#include "BSP_Driver.h"

static uint16_t 		UART_data_len = 0;            						///< uart receive data len
static uint32_t    	rak_Timer=0;	       											///< counter for timeout
volatile uint8_t UART_RecieveDataFlag = 0;										///< flag indicates uart if get data
uint16_t UART_recvDataLen=0;																	///< for Transparent transmission

/**
 *  brief.			ISR to handle UART Channel 1 interrupt event
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void UART1_IRQHandler(void)
{
    uint8_t bInChar[1]= {0};

    if(UART1->ISR & UART_ISR_RDA_INT_Msk)                              ///< Check if receive interrupt
    {
        _TIMER_RESET(TIMER1);
        if(_UART_IS_RX_READY(UART1))                                	///< Check if received data avaliable
        {
            while (UART1->FSR & UART_FSR_RX_EMPTY_Msk);               ///< Wait until an avaliable char present in RX FIFO
            bInChar[0] = UART1->RBR;                               		///< Read the char
            if(UART_data_len < RXBUFSIZE)                        			///< Check if buffer full
            {
                uCmdRspFrame.uCmdRspBuf[UART_data_len] = bInChar[0];  ///< Enqueue the character
                UART_data_len++;
            }
        }
        _TIMER_START(TIMER1, TIMER_TCSR_CEN_Msk | TIMER_TCSR_IE_Msk | TIMER_TCSR_MODE_PERIODIC | TIMER_TCSR_TDR_EN_Msk ,1);
    }
}

/**
 *  brief.			rak_uart_send
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
uint8_t rak_uart_send(char *tx_buf,uint16_t buflen)
{
    uint16_t i;
    for (i=0; i<buflen; i++) {
        while((UART1->FSR & UART_FSR_TX_FULL_Msk) != 0);                   //发送FIFO满时等待//en:Wait until UART transmit FIFO is not full
        UART1->THR = (uint8_t) tx_buf[i];                           //通过UART0发送一个字符//en:Transmit a char via UART0
    }
    return 0;
}

/**
 *  brief.			TMR1_IRQHandler
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void TMR1_IRQHandler(void)
{
//    uint8_t status=0;
    /* Clear TIMER0 Timeout Interrupt Flag */
    _TIMER_CLEAR_CMP_INT_FLAG(TIMER1);
    _TIMER_RESET(TIMER1);
    UART_RecieveDataFlag = TRUE;
    UART_recvDataLen = UART_data_len;
    UART_data_len=0;

}


/**
 *  brief.			Command response timeout
 * @param[in]		timeout
 * @param[out]	none
 * @return			none
 */
int8_t RAK_RESPONSE_TIMEOUT(uint32_t timeOut)
{
    int8_t   retval=0;

    RAK_RESET_TIMER;
    while((UART_RecieveDataFlag == FALSE) && (RAK_INC_TIMER<=timeOut));
    if (RAK_INC_TIMER > timeOut)
    {
        retval = RAK_FAIL;
    }
    return retval;
}

/**
 *  brief.			rak_clearPktFlag
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void  rak_clearPktFlag(void)
{
    UART_RecieveDataFlag =false;
}

/**
 *  brief.			RESET CPU core and IP
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void CHIPIP_RST(void)
{
    /* RESET CPU core and IP */
    SYS->IPRSTC1 |= SYS_IPRSTC1_CHIP_RST_Msk;
}

/**
 *  brief.			RESET RAK WIFI MODULE
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void reset_module(void)
{
    RESET_PORT_PIN=0;
    SYS_SysTickDelay(10000);
    RESET_PORT_PIN=1;
}

/**
 *  brief.			delay for ms
 * @param[in]		ms
 * @param[out]	none
 * @return			none
 */
void delay_ms(int32_t ms)
{
    int32_t i;

    for(i=0; i<ms; i++)
        SYS_SysTickDelay(1000);
}


