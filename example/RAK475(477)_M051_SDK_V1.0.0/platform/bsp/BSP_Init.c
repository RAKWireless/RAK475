/**
* @file 		BSP_Init.c
* @author		harry
* @date			2016/6/1
* @version	1.0.0
* @par Copyright (c):
* 					rakwireless
* @par History:
*	version: author, date, desc\n
*/


#include "BSP_Init.h"

/**
 *  brief.			Init System Clock
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void SYS_Init(void)
{

	// Unlock protected registers 
    SYS_UnlockReg();

    // Enable XTL12M clock 
    SYSCLK->PWRCON |= SYSCLK_PWRCON_XTL12M_EN_Msk;

    // Waiting for XTL12M clock ready 
    SYS_WaitingForClockReady(SYSCLK_CLKSTATUS_XTL12M_STB_Msk);

	/* Switch HCLK clock source to XTL12M */
    SYSCLK->CLKSEL0 =SYSCLK_CLKSEL0_STCLK_XTAL | SYSCLK_CLKSEL0_HCLK_XTAL;

    SYSCLK->APBCLK = SYSCLK_APBCLK_UART0_EN_Msk | SYSCLK_APBCLK_UART1_EN_Msk |SYSCLK_APBCLK_TMR1_EN_Msk 	;
								
	SYSCLK->CLKSEL1 = SYSCLK_CLKSEL1_UART_XTAL | SYSCLK_CLKSEL1_TMR1_XTAL;

		 
//  SYS_LockReg();
}

/**
 *  brief.			Init UART1
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void UART1_Init(void)
{

	SYS->IPRSTC2 |= SYS_IPRSTC2_UART1_RST_Msk; 
	SYS->IPRSTC2 &= ~SYS_IPRSTC2_UART1_RST_Msk;
   /* Set P1 multi-function pins for UART1 RXD and TXD  */
    SYS->P1_MFP |= SYS_MFP_P12_RXD1 | SYS_MFP_P13_TXD1;

	UART1->BAUD = UART_BAUD_MODE2 | UART_BAUD_DIV_MODE2(__XTAL,115200); // __XTAL
    _UART_SET_DATA_FORMAT(UART1, UART_WORD_LEN_8 | UART_PARITY_NONE | UART_STOP_BIT_1);

    /* Enable Interrupt and install the call back function */
    _UART_ENABLE_INT(UART1, (UART_IER_RDA_IEN_Msk | UART_IER_RTO_IEN_Msk));
    NVIC_EnableIRQ(UART1_IRQn);	
}

/**
 *  brief.			Init UART0--for debug printf
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void UART0_Init(void)
{
	SYS->IPRSTC2 |= SYS_IPRSTC2_UART0_RST_Msk; 
	SYS->IPRSTC2 &= ~SYS_IPRSTC2_UART0_RST_Msk;
   /* Set P1 multi-function pins for UART1 RXD and TXD  */
    SYS->P3_MFP |= SYS_MFP_P30_RXD0 | SYS_MFP_P31_TXD0;

	UART0->BAUD = UART_BAUD_MODE2 | UART_BAUD_DIV_MODE2(__XTAL,115200); // __XTAL
    _UART_SET_DATA_FORMAT(UART0, UART_WORD_LEN_8 | UART_PARITY_NONE | UART_STOP_BIT_1);

//    /* Enable Interrupt and install the call back function */
//    _UART_ENABLE_INT(UART0, (UART_IER_RDA_IEN_Msk | UART_IER_RTO_IEN_Msk));
//    NVIC_EnableIRQ(UART1_IRQn);	
}

/**
 *  brief.			Init UART1
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void GPIO_Init(void)
{
	
    /* Reset pin */
	_GPIO_SET_PIN_MODE(RESET_PORT, RESET_PIN, GPIO_PMD_OUTPUT);
	/* Debug pin */
	_GPIO_SET_PIN_MODE(RAK_DEBUG_PORT, RAK_DEBUG_PIN, GPIO_PMD_OUTPUT);
}

/**
 *  brief.			TIMER1_Init
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */	 	
void TIMER1_Init(void)
{
    _TIMER_RESET(TIMER1);
	 /* Enable TIMER1 NVIC */
    NVIC_EnableIRQ(TMR1_IRQn);
	/* To Configure TCMPR values based on Timer clock source and pre-scale value */
    TIMER1->TCMPR = __XTAL*100/115200;      // For 1 tick per second when using external 12MHz (Prescale = 1)
	/* Configure TCMP values of TIMER1 */
   _TIMER_RESET(TIMER1);
}

/**
 *  brief.			BSP_Init
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */	 	
void host_platformInit(void)
{
	SYS_Init();
	GPIO_Init();	   	// initialize gpio
	UART0_Init();  		// initialize debug uart
	UART1_Init();	   	// initialize communication uart
	TIMER1_Init();	  // initialize timer

}
