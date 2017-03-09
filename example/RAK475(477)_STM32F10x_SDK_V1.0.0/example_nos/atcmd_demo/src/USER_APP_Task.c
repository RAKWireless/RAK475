/**
* @file 		USER_APP_Task.c
* @author		harry
* @date			2016/6/1
* @version	1.0.0
* @par Copyright (c):
* 					rakwireless
* @par History:
*	version: author, date, desc\n
*/

#include "BSP_Driver.h"

uint8_t  wait_WPSbuttonPress=0;             //do wps or not
uint8_t	 wait_EasyConfigbuttonPress=0;			//do easy_config or not

/**
 *  brief.			Recv_dataHandle
 * @param[in]		none
 * @param[out]	none
 * @return			RAK_SUCCESS
 *							RAK_FAIL
 */
int8_t Recv_dataHandle(void)
{
    uint16_t  recvLen=0;
    uint16_t  dest_port=0;
    uint32_t  dest_ip=0;

#if (RAK_MODULE_WORK_MODE==ASSIST_CMD_TYPE)
    if(strncmp((char*)uCmdRspFrame.recvdataFrame.recvdata.recvheader,RAK_RECIEVE_DATA_CMD,strlen(RAK_RECIEVE_DATA_CMD))==0)
    {
        dest_port=uCmdRspFrame.recvdataFrame.recvdata.destPort[0]|
                  uCmdRspFrame.recvdataFrame.recvdata.destPort[1]<<8;

        dest_ip  =uCmdRspFrame.recvdataFrame.recvdata.destIp[0]|
                  uCmdRspFrame.recvdataFrame.recvdata.destIp[1]<<8 |
                  uCmdRspFrame.recvdataFrame.recvdata.destIp[2]<<16|
                  uCmdRspFrame.recvdataFrame.recvdata.destIp[3]<<24;

        recvLen=uCmdRspFrame.recvdataFrame.recvdata.recDataLen[0]+uCmdRspFrame.recvdataFrame.recvdata.recDataLen[1]*256;
        if(uCmdRspFrame.recvdataFrame.recvdata.socketID==SOCKETA_ID) //receive data from socketA
        {
            rak_send_data(SOCKETA_ID,dest_port,dest_ip,recvLen,uCmdRspFrame.recvdataFrame.recvdata.recvdataBuf);

        } else if(uCmdRspFrame.recvdataFrame.recvdata.socketID==SOCKETB_ID) //receive data from socketB
        {
            rak_send_data(SOCKETB_ID,dest_port,dest_ip,recvLen,uCmdRspFrame.recvdataFrame.recvdata.recvdataBuf);
        }
        return RAK_SUCCESS;

    } else
    {
        return RAK_FAIL;		 //unexpected response
    }

#elif (RAK_MODULE_WORK_MODE==EASY_TXRX_TYPE)
		/* double socket Transparent transmission, need to parse "S0" or "S1" head*/
    if(strncmp((char*)uCmdRspFrame.recvdataFrame.recvdata.recvheader,SOCKETA_HDER,strlen(SOCKETA_HDER))==0)
    {
        rak_uart_send(SOCKETA_HDER,strlen(SOCKETA_HDER));
        rak_uart_send(&uCmdRspFrame.uCmdRspBuf[2],UART_recvDataLen-2);
    } else if(strncmp((char*)uCmdRspFrame.recvdataFrame.recvdata.recvheader,SOCKETB_HDER,strlen(SOCKETB_HDER))==0)
    {
        rak_uart_send(SOCKETA_HDER,strlen(SOCKETB_HDER));
        rak_uart_send(&uCmdRspFrame.uCmdRspBuf[2],UART_recvDataLen-2);
    }
    /* is single socket*/
    else
    {
        rak_uart_send(uCmdRspFrame.uCmdRspBuf,UART_recvDataLen);
    }
    return RAK_SUCCESS;
#endif

}


int main(void)
{
    int8_t   retval;

		host_platformInit();

    reset_module();	   	//reset wifi module

#if RAK_MODULE_WORK_MODE==ASSIST_CMD_TYPE
    retval=Module_Enter_CmdMode();
    if(retval<0)
    {
        return retval;
    }
#endif


    while(1)
    {
        if(UART_RecieveDataFlag == true)						 //handle received data
        {
            UART_RecieveDataFlag = false;
            Recv_dataHandle();
        }
				/* 	if customer's design did not use module's wps or default PIN, 
						then the host can use AT Command trigger the config event 
				*/
#if   RAK_MODULE_WORK_MODE==ASSIST_CMD_TYPE
        if(wait_WPSbuttonPress)
        {
            wait_WPSbuttonPress=0;
            Send_WPS_Cmd();
        }
        if(wait_EasyConfigbuttonPress)
        {
            wait_EasyConfigbuttonPress=0;
            Send_EasyConfig_Cmd();
        }
#endif
        delay_ms(2);

    }
}


