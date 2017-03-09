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

typedef uint32_t rw_stamp_t;

static uint16_t 		UART_data_len = 0;            						///< uart receive data len
volatile uint8_t UART_RecieveDataFlag = 0;										///< flag indicates uart if get data
uint16_t UART_recvDataLen=0;																	///< for Transparent transmission

rw_stamp_t get_stamp(void);

/**
 *  brief.			ISR to handle UART Channel 1 interrupt event
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void USART2_IRQHandler(void)
{
    uint8_t bInChar[1]= {0};
		uint32_t temp = 0;  
    if(USART_GetITStatus(USART2, USART_IT_RXNE) != RESET)					///< Check if received data avaliable
    {
				TIM_SetCounter(TIM3, 0);
				TIM_Cmd(TIM3, DISABLE);
				bInChar[0] = ( uint8_t )USART_ReceiveData(USART2);
        if(UART_data_len < RXBUFSIZE)                        			///< Check if buffer full
        {
            uCmdRspFrame.uCmdRspBuf[UART_data_len] = bInChar[0];  ///< Enqueue the character
            UART_data_len++;
        }
				TIM_Cmd(TIM3, ENABLE);
        TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);
 	       
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
        while((USART2->SR&0X40)==0);
        USART2->DR = (u8) tx_buf[i];
    }
    return 0;
}

/**
 *  brief.			TMR1_IRQHandler
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void TIM3_IRQHandler (void)
{
    if(TIM_GetITStatus(TIM3,TIM_IT_Update)!=RESET) {

        UART_RecieveDataFlag = true;
        UART_recvDataLen = UART_data_len;
        UART_data_len=0;
			
				TIM_SetCounter(TIM3, 0);
				TIM_Cmd(TIM3, DISABLE);
        TIM_ClearFlag(TIM3, TIM_FLAG_Update);
        TIM_ClearITPendingBit(TIM3, TIM_IT_Update);
    }
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
    unsigned long expire = get_stamp() + timeOut;
    while(get_stamp() < expire) {
        if(UART_RecieveDataFlag == true) break;
    }
    if(UART_RecieveDataFlag == false) {
        return RAK_FAIL;
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
 *  brief.			RESET RAK WIFI MODULE
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void reset_module(void)
{
    GPIO_WriteBit(WIFI_PWD_GPIO_PORT,WIFI_PWD_PIN, Bit_RESET);
    delay_ms(50);
    GPIO_WriteBit(WIFI_PWD_GPIO_PORT,WIFI_PWD_PIN, Bit_SET);
}

/**
 *  brief.			delay for ms
 * @param[in]		ms
 * @param[out]	none
 * @return			none
 */
void delay_ms(int count)
{
    int time =HAL_GetTick() + count;
    while(HAL_GetTick()<time);
}

rw_stamp_t get_stamp(void)
{
    return HAL_GetTick();
}

bool is_stamp_passed(rw_stamp_t* stamp) {
    return (get_stamp() > (*stamp));
}

