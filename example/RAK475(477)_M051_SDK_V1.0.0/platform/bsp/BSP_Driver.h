#ifndef __BSP_DRIVER_H__
#define __BSP_DRIVER_H__

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>
#include "M051Series.h"
#include "rak_config.h"
#include "rak_global.h"
#include "rak_uart_api.h"
#include "BSP_Init.h"

uint8_t  	rak_uart_send(char *tx_buf,uint16_t buflen);
int8_t 		RAK_RESPONSE_TIMEOUT(uint32_t timeOut);
void  		rak_clearPktFlag(void);
void    	reset_module(void);
void    	delay_ms(int32_t ms);

int8_t  	Module_Enter_CmdMode(void);
int8   		Send_WPS_Cmd(void);
int8   		Send_EasyConfig_Cmd(void);


extern  	volatile uint8_t  UART_RecieveDataFlag;
extern  	uint16_t  UART_recvDataLen;
extern 		rak_uCmdRsp	      uCmdRspFrame;

#endif
