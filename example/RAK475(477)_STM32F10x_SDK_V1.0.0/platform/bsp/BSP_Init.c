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

#if   defined ( __CC_ARM )
#ifdef __GNUC__
/* With GCC/RAISONANCE, small printf (option LD Linker->Libraries->Small printf
   set to 'Yes') calls __io_putchar() */
#define PUTCHAR_PROTOTYPE int __io_putchar(int ch)
#else
#define PUTCHAR_PROTOTYPE int fputc(int ch, FILE *f)
#endif /* __GNUC__ */
/**
  * @brief  Retargets the C library printf function to the USART.
  * @param  None
  * @retval None
  */
PUTCHAR_PROTOTYPE
{
    /* Place your implementation of fputc here */
    /* e.g. write a character to the EVAL_COM1 and Loop until the end of transmission */
    USART_SendData(PRINT_USART, (uint16_t) ch);

    /* Loop until the end of transmission */
    while (USART_GetFlagStatus(PRINT_USART, USART_FLAG_TC) == RESET)
    {};

    return ch;
}
#endif

/**
 *  brief.			initialize wifi reset pin
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void Wifi_GPIO_Init(void)
{
    GPIO_InitTypeDef GPIO_InitStructure;

    RCC_APB2PeriphClockCmd(WIFI_PWD_GPIO_CLK, ENABLE);
    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
    GPIO_InitStructure.GPIO_Pin = WIFI_PWD_PIN;
    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
    GPIO_Init(WIFI_PWD_GPIO_PORT, &GPIO_InitStructure);
}

/**
 *  brief.			initialize print uart
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void Print_UART_Init(void)
{
    GPIO_InitTypeDef  GPIO_InitStructure;
    USART_InitTypeDef USART_InitStructure;

    RCC_APB2PeriphClockCmd(PRINT_USART_CLK, ENABLE);
    RCC_APB2PeriphClockCmd(PRINT_USART_TX_GPIO_CLK | PRINT_USART_RX_GPIO_CLK, ENABLE);

    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
    GPIO_InitStructure.GPIO_Pin = PRINT_USART_TX_PIN;
    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
    GPIO_Init(PRINT_USART_TX_GPIO_PORT,&GPIO_InitStructure);

    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
    GPIO_InitStructure.GPIO_Pin = PRINT_USART_RX_PIN;
    GPIO_Init(PRINT_USART_RX_GPIO_PORT, &GPIO_InitStructure);

    USART_InitStructure.USART_BaudRate = 115200;
    USART_InitStructure.USART_WordLength = USART_WordLength_8b;
    USART_InitStructure.USART_StopBits = USART_StopBits_1;
    USART_InitStructure.USART_Parity = USART_Parity_No;
    USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
    USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;

    USART_Init(PRINT_USART, &USART_InitStructure);
    USART_Cmd(PRINT_USART, ENABLE);

}

/**
 *  brief.			initialize wifi uart
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void  Wifi_UART_Init(uint32_t baud_rate)
{
    GPIO_InitTypeDef GPIO_InitStructure;
    USART_InitTypeDef USART_InitStructure;
    NVIC_InitTypeDef NVIC_InitStructure;
		
		USART_DeInit(WIFI_USART);
    RCC_APB2PeriphClockCmd(WIFI_USART_TX_GPIO_CLK | WIFI_USART_RX_GPIO_CLK, ENABLE);
    RCC_APB1PeriphClockCmd(WIFI_USART_CLK, ENABLE);

    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
    GPIO_InitStructure.GPIO_Pin = WIFI_USART_TX_PIN;
    GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
    GPIO_Init(WIFI_USART_TX_GPIO_PORT, &GPIO_InitStructure);

    GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;//GPIO_Mode_IN_FLOATING;
    GPIO_InitStructure.GPIO_Pin = WIFI_USART_RX_PIN;
    GPIO_Init(WIFI_USART_RX_GPIO_PORT, &GPIO_InitStructure);

    USART_InitStructure.USART_BaudRate = baud_rate;
    USART_InitStructure.USART_WordLength = USART_WordLength_8b;
    USART_InitStructure.USART_StopBits = USART_StopBits_1;
    USART_InitStructure.USART_Parity = USART_Parity_No;
    USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
    USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
    USART_Init(WIFI_USART, &USART_InitStructure);

    NVIC_InitStructure.NVIC_IRQChannel = USART2_IRQn;
    NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
    NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;
    NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
    NVIC_Init(&NVIC_InitStructure);

    USART_ITConfig(WIFI_USART, USART_IT_RXNE, ENABLE);
    USART_Cmd(WIFI_USART, ENABLE);
}


void TIM3_Init(void)
{
  TIM_TimeBaseInitTypeDef 	 TIM_TimeBaseStructure;
  NVIC_InitTypeDef NVIC_InitStructure;
	
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);
	TIM_DeInit(TIM3);
	//couter clk=36MHz/36 = 1MHz	-- 20ms
  TIM_TimeBaseStructure.TIM_Prescaler = 36-1;
  TIM_TimeBaseStructure.TIM_ClockDivision = TIM_CKD_DIV1;    
  TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;
  TIM_TimeBaseStructure.TIM_Period = 20000;
  TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);
       
	
	  /* Enable the TIM3 Interrupt */
  NVIC_InitStructure.NVIC_IRQChannel = TIM3_IRQn;
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 1;
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 1;
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
  NVIC_Init(&NVIC_InitStructure); 
}

void host_platformInit(void)
{
    SystemCoreClockUpdate();
    SysTick_Config(SystemCoreClock/1000);
		
		Wifi_GPIO_Init();
		TIM3_Init();
		Print_UART_Init();
    Wifi_UART_Init(115200);
}

