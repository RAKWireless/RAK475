/**
* @file 		RAK_WIFI_Driver.c
* @author		harry
* @date			2016/6/1
* @version	1.0.0
* @par Copyright (c):
* 					rakwireless
* @par History:
*	version: author, date, desc\n
*/

#include "BSP_Driver.h"

rak_uCmdRsp	      uCmdRspFrame;		   ///< AT Command response structure

int8 rak_open_cmdMode(void);

int8_t  Module_Enter_CmdMode(void)
{
    int8_t   retval=0;
    uint8_t scan_nums=0;
    int i;

    /* enter cmd mode*/
    retval=rak_open_cmdMode();
    if(retval<0)
    {
        printf("rak_open_cmdMode err=%d\r\n", retval);
        return retval;
    }
    printf("enter cmd mode ok\r\n");

    /* query version */
    printf("query version...\r\n");
    rak_query_fwversion();
    retval=RAK_RESPONSE_TIMEOUT(RAK_QRYFWTIMEOUT);
    if(retval<0)
    {
        printf("rak_query_fwversion timeout\r\n");
        return retval;
    } else
    {
        rak_clearPktFlag();
        if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
        {
            printf("version=%s", uCmdRspFrame.qryFwversionFrameRcv.qryframe.hostFwversion);
            retval= RAK_SUCCESS;
        } else
        {
            printf("rak_query_fwversion err\r\n");
            while(1);
        }
    }

    /* read userConfig */
    printf("read userConfig...\r\n");
    rak_read_userConfig();
    retval=RAK_RESPONSE_TIMEOUT(RAK_RDCFGTIMEOUT);
    if(retval<0)
    {
        printf("rak_read_userConfig timeout\r\n");
        return retval;
    } else
    {
        rak_clearPktFlag();
        if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
        {
						printf("%s", (char *)uCmdRspFrame.uCmdRspBuf);
            retval= RAK_SUCCESS;
        } else
        {
            printf("rak_read_userConfig err\r\n");
            while(1);
        }
    }

SCAN:
    /* scan */
    printf("scan...\r\n");
    rak_scan_ap(0, NULL);
    retval=RAK_RESPONSE_TIMEOUT(RAK_SCANTIMEOUT);
    if(retval<0)
    {
        printf("rak_scan_ap timeout\r\n");
        return retval;
    } else
    {
        rak_clearPktFlag();
        if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
        {
            scan_nums = uCmdRspFrame.scanResponse.scanOkframe.scanCount;
            printf("scan aps=%d\r\n", scan_nums);
            retval= RAK_SUCCESS;
        } else
        {
            printf("rak_scan_ap err\r\n");
            goto SCAN;
            //while(1);
        }
    }

    /* get scan */
    printf("get scan...\r\n");
		rak_get_scan(scan_nums);
    retval=RAK_RESPONSE_TIMEOUT(RAK_GETSCANTIMEOUT);
    if(retval<0)
    {
        printf("rak_get_scan timeout\r\n");
        return retval;
    } else
    {
        rak_clearPktFlag();
        if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
        {
            scan_nums = scan_nums<RAK_AP_SCANNED_MAX?scan_nums:RAK_AP_SCANNED_MAX;
            for(i=0; i<scan_nums; i++) {
                printf("ssid=%s\r\n", uCmdRspFrame.getscanResponse.getOkframe.strScanInfo[i].ssid);
            }
            retval= RAK_SUCCESS;
        } else
        {
            printf("rak_get_scan err\r\n");
            while(1);
        }
    }
    
    /* wait module connected to ap*/
    do
    {
        rak_query_constatus();
        retval=RAK_RESPONSE_TIMEOUT(RAK_QCONSATUSTIMEOUT);
        if(retval<0)
        {
            printf("rak_query_constatus timeout\r\n");
            return retval;
        } else
        {
            rak_clearPktFlag();
            if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0 &&(uCmdRspFrame.qryconstatusResponse.qryconstatusframe.status==0x01))
            {
                retval= RAK_SUCCESS;
            } else
            {
                retval= RAK_FAIL;
                printf("net dis\r\n");
            }
        }
        delay_ms(200);
    } while(retval!= RAK_SUCCESS);

    printf("net conn ok\r\n");

    /* query module's ip info*/
    do
    {
        rak_query_ipconifg();
        retval=RAK_RESPONSE_TIMEOUT(RAK_QIPCFGTIMEOUT);
        if(retval<0)
        {
            printf("rak_query_ipconifg timeout\r\n");
            return retval;
        } else
        {
            rak_clearPktFlag();
            if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
            {
                retval= RAK_SUCCESS;
            }
        }
        delay_ms(200);
    } while(retval!= RAK_SUCCESS);
		
    printf("ip query ok\r\n");

QRY_NET:
    /* query module's current net info*/
    memset((char *)&uCmdRspFrame, 0, sizeof(uCmdRspFrame));
    rak_query_netinfo();
    retval=RAK_RESPONSE_TIMEOUT(RAK_QRYNETINFOTIMEOUT);
    if(retval<0)
    {
        printf("rak_query_netinfo timeout\r\n");
        return retval;
    } else
    {
        rak_clearPktFlag();
        if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
        {
						if(strstr((char *)&uCmdRspFrame, "sta_ip=192.168.78.2")) {
							delay_ms(100);
							goto QRY_NET;
						}
						printf("%s", (char *)uCmdRspFrame.uCmdRspBuf);
            retval= RAK_SUCCESS;
        }
    }

    printf("query netinfo ok\r\n");
    return RAK_SUCCESS;
}

/**
 *  brief.			enter AT command mode from easytxrx mode
 * @param[in]		none
 * @param[out]	none
 * @return			0 success
 *							RAK_FAIL					
 */
int8 rak_open_cmdMode(void)
{
    int8	     retval=0;
    uint8_t	     retry_times=0;	   //重试次数设置

    while(1)
    {
        if(++retry_times>10)
        {
            retval= RAK_FAIL;
            break;
        }

        rak_uart_send("+++",3);//发送打开模块命令接口请求

        retval=RAK_RESPONSE_TIMEOUT(RAK_200MS_TIMEOUT);
        if(retval<0)
        {
            retval= RAK_FAIL;
        }
        else
        {
            rak_clearPktFlag();
            if(uCmdRspFrame.uCmdRspBuf[0]=='U')
            {
                rak_uart_send("U",1);
                retval=RAK_RESPONSE_TIMEOUT(RAK_3S_TIMEOUT);
                if(retval<0)
                {
                    retval= RAK_FAIL;
                }
                else
                {
                    rak_clearPktFlag();
                    if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
                    {
                        retval= RAK_SUCCESS;
                    } else
                    {
                        retval= RAK_FAIL;
                    }
                }
            } 
						else
            {
                retval= RAK_FAIL;
            }
        }
				
				if(retval == RAK_SUCCESS) break;
    }

    return retval;
}

int8   Send_WPS_Cmd(void)
{
    int8_t   retval=0;
    rak_wps_config();
		retval=RAK_RESPONSE_TIMEOUT(RAK_WPSTIMEOUT);
		if(retval<0)
		{
				return retval;
		} else
		{
				rak_clearPktFlag();
				if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
				{
						printf("enter wps mode ok\r\n");
						retval = RAK_SUCCESS;
				}
		}

    return RAK_SUCCESS;
}


int8   Send_EasyConfig_Cmd(void)
{
    int8_t   retval=0;
    rak_easy_config();

		retval=RAK_RESPONSE_TIMEOUT(RAK_EASYCFGTIMEOUT);
		if(retval<0)
		{
				return retval;
		} else
		{
				rak_clearPktFlag();
				if(strncmp((char *)uCmdRspFrame.uCmdRspBuf,"OK",2) == 0)
				{
						printf("enter easy_config mode ok\r\n");
						retval= RAK_SUCCESS;
				}
		}
    return RAK_SUCCESS;
}
