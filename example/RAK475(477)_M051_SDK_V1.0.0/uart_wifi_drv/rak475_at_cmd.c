/**
* @file 		rak475_at_cmd.c
* @brief		this is a uart driver for ATcommand wifi.
* @details	This is the detail description.
* @author		harry
* @date			2016/6/1
* @version	1.0.0
* @par Copyright (c):
* 					rakwireless
* @par History:
*	version: author, date, desc\n
*/
#include "BSP_Driver.h"

#define htons(n) 				(((n & 0xff) << 8) | ((n & 0xff00) >> 8))
#define htonl(n) 				(((n & 0xff) << 24) | ((n & 0xff00) << 8) | ((n & 0xff0000UL) >> 8) | ((n & 0xff000000UL) >> 24))
#define ntohs(n) 				htons(n)
#define ntohl(n) 				htonl(n)


/**
 *  brief.			query module's mac address
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_macaddr(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+mac\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			enter easyMode from atCmdMode
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_easy_txrxMode(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+easy_txrx\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query fw version
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_fwversion(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+version\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			reset module
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_reset(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+reset\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			restore to factory mode
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_restore(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+restore\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			read user config
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_read_userConfig(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+read_config\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			write user config
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_write_userConfig(void)
{
    char cmd[30]="";
    char data_stream[1000]= {
        /*0:sta 1:ap 2:ap+sta*/
        "wlan_mode=1"\
        /*ap param*/
        "&ap_ssid=RAK475_AP&ap_channel=6&ap_sec_mode=0&ap_psk=&ap_max_clts=3&ap_bdcast=1&ap_ip=192.168.7.1"\
        /*sta param*/
        "&sta_ssid=RAK_STATION&sta_sec_mode=1&sta_psk=1234567890&sta_dhcp=1&sta_sec_type=8&sta_bssid=00:00:00:00:00:00"\
        "&sta_ip=192.168.78.2&sta_netmask=255.255.255.0&sta_gateway=192.168.78.1&sta_dns1=0.0.0.0&sta_dns2=0.0.0.0"\
        /*uart param*/
        "&uart_baudrate=115200&uart_datalen=8&uart_parity_en=0&uart_stoplen=1&uart_rtscts_en=0&uart_timeout=20&uart_recvlenout=500"\
        /*power param*/
        "&power_mode=0"\
        /*socket param*/
        "&socket_multi_en=0"\
        "&socketA_type=1&socketA_tls_v=0&socketA_tls_ca=0&socketA_tls_clt=0&socketA_max_clts=3"\
        "&socketA_destip=192.168.1.1&socketA_destport=80&socketA_localport=25000&socketA_tcp_timeout=0&socketA_tcp_reconval=3"\
        "&socketB_type=1&socketB_max_clts=3"\
        "&socketB_destip=192.168.1.1&socketB_destport=80&socketB_localport=25000&socketB_tcp_timeout=0&socketB_tcp_reconval=3"\
        /*manage param*/
        "&user_name=admin&user_password=admin&module_name=RAK475"
    };
    sprintf(cmd, "at+write_config=%d", strlen(data_stream));
    rak_uart_send(cmd, strlen(cmd));
    rak_uart_send(data_stream, strlen(data_stream));
    rak_uart_send("\r\n", 2);
}

/**
 *  brief.			read factory config
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_read_restoreConfig(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+read_restoreconfig\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			write factory config
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_write_restoreConfig(void)
{
    char cmd[30]="";
    char data_stream[1000]= {
        /*0:sta 1:ap 2:ap+sta*/
        "wlan_mode=1"\
        /*ap param*/
        "&ap_ssid=RAK475_AP&ap_channel=6&ap_sec_mode=0&ap_psk=&ap_max_clts=3&ap_bdcast=1&ap_ip=192.168.7.1"\
        /*sta param*/
        "&sta_ssid=RAK_STATION&sta_sec_mode=1&sta_psk=1234567890&sta_dhcp=1&sta_sec_type=8&sta_bssid=00:00:00:00:00:00"\
        "&sta_ip=192.168.78.2&sta_netmask=255.255.255.0&sta_gateway=192.168.78.1&sta_dns1=0.0.0.0&sta_dns2=0.0.0.0"\
        /*uart param*/
        "&uart_baudrate=115200&uart_datalen=8&uart_parity_en=0&uart_stoplen=1&uart_rtscts_en=0&uart_timeout=20&uart_recvlenout=500"\
        /*power param*/
        "&power_mode=0"\
        /*socket param*/
        "&socket_multi_en=0"\
        "&socketA_type=1&socketA_tls_v=0&socketA_tls_ca=0&socketA_tls_clt=0&socketA_max_clts=3"\
        "&socketA_destip=192.168.1.1&socketA_destport=80&socketA_localport=25000&socketA_tcp_timeout=0&socketA_tcp_reconval=3"\
        "&socketB_type=1&socketB_max_clts=3"\
        "&socketB_destip=192.168.1.1&socketB_destport=80&socketB_localport=25000&socketB_tcp_timeout=0&socketB_tcp_reconval=3"\
        /*manage param*/
        "&user_name=admin&user_password=admin&module_name=RAK475"
    };
    sprintf(cmd, "at+write_restoreconfig=%d", strlen(data_stream));
    rak_uart_send(cmd, strlen(cmd));
    rak_uart_send(data_stream, strlen(data_stream));
    rak_uart_send("\r\n", 2);
}

/**
 *  brief.			copy user's configuration to factory's
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_copy_uConfig(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+copy_cfg\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query sta's conn status
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_constatus(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+con_status\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query ap's conn status
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_apStatus(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+ap_status\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query rssi -- sta mode only
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_rssi(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+rssi\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			scan ap
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_scan_ap(uint8_t channel, char *ssid)
{
    char cmd[50]="";
		if(ssid == NULL) {
			sprintf(cmd, "at+scan=%d\r\n", channel);
		} else {
			sprintf(cmd, "at+scan=%d,%s\r\n", channel, ssid);
		}
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			getscan ap
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_get_scan(uint8_t ap_num)
{
    char cmd[30]="";
    sprintf(cmd, "at+get_scan=%d\r\n", ap_num);
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			easy_config
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_easy_config(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+easy_config\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			wps_config
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_wps_config(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+wps\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query module's ip info
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_ipconifg(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+ipconfig\r\n");
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			ping
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_ping(char *dest_ip)
{
    char cmd[50]="";
    sprintf(cmd, "at+ping=%s\r\n", dest_ip);
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query tcp status
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_tcpstatus(uint8_t sock_id)
{
    char cmd[30]="";
    sprintf(cmd, "at+tcp_stauts=%d\r\n", sock_id);	//single socket or socketA
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			query current net info
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_query_netinfo(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+net_info\r\n");	//single socket or socketA
    rak_uart_send(cmd, strlen(cmd));
}

/**
 *  brief.			send data via socket
 * @param[in]		sock_fd
 * @param[in]		dest_port
 * @param[in]		dest_ip
 * @param[in]		send_len
 * @param[in]		buf	-- store data to send
 * @param[out]	none
 * @return			none
 * @par     		cmd prototype:
 *								at+send_data=<uuid>,<dest_port>,<dest_ip>,<data_length> ,<data_stream>\r\n
 *							description:
 *								if is tcp or udp client, the dest_port and dest_ip can be ignored, for example:
 *								at+send_data=0,0,0,10,1234567890\r\n
 *								if is ludp(udp server), the dest_port and dest_ip need to be filled because the
 *								module does not know the remote ip and port.									
 */
void rak_send_data(uint8 sock_fd, uint16 dest_port, uint32 dest_ip, uint16 send_len, uint8 *buf)
{
    char cmd[100]="";
    uint8_t ip_buf[16];

    if(dest_ip != 0) {
        dest_ip = htonl(dest_ip);
        rak_bytes4ToAsciiDotAddr((uint8_t *)&dest_ip, ip_buf);
        sprintf(cmd,"at+send_data=%d,%d,%s,%d,", sock_fd, dest_port, ip_buf, send_len);
    } else {
        sprintf(cmd,"at+send_data=%d,%d,%d,%d,", sock_fd, dest_port, dest_ip, send_len);
    }
    rak_uart_send(cmd, strlen(cmd));
    rak_uart_send((char *)buf, send_len);
    rak_uart_send("\r\n",2);
}

/**
 *  brief.			rak_nvm_write-- usr flash api
 * @param[in]		addr
 * @param[in]		len
 * @param[in]		data
 * @param[out]	none
 * @return			none
 */
void rak_nvm_write(uint32_t addr, uint16_t len, char *data)
{
    char cmd[30]="";
    sprintf(cmd, "at+nvm_write=%d,%d,", addr, len);
    rak_uart_send(cmd,strlen(cmd));
    rak_uart_send(data, len);
    rak_uart_send("\r\n",2);
}

/**
 *  brief.			rak_nvm_read -- usr flash api
 * @param[in]		addr
 * @param[in]		len
 * @param[out]	none
 * @return			none
 */
void rak_nvm_read(uint32_t addr, uint16_t len)
{
    char cmd[30]="";
    sprintf(cmd, "at+nvm_read=%d,%d\r\n", addr, len);
    rak_uart_send(cmd,strlen(cmd));
}

/**
 *  brief.			set cert
 * @param[in]		cert_type
 *							- 0:SSL Client Private Key
 *							- 1:SSL Client Certificate
 *							- 2:SSL CA Certificate
 * @param[in]		len - cert size
 * @param[in]		data - cert data
 * @param[out]	none
 * @return			none
 */
void rak_set_cert(uint8_t cert_type, uint16_t len, uint8_t *data)
{
    char cmd[30]="";
    sprintf(cmd, "at+set_cert=%d,%d,", cert_type, len);
    rak_uart_send(cmd,strlen(cmd));
    rak_uart_send(data, len);
    rak_uart_send("\r\n", 2);
}

/**
 *  brief.			read_userlist_num
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_read_userlist_num()
{
    char cmd[30]="";
    sprintf(cmd, "at+read_userlist_num\r\n");
    rak_uart_send(cmd,strlen(cmd));

}

/**
 *  brief.			read_userlist
 * @param[in]		index
 * @param[out]	none
 * @return			none
 */
void rak_read_userlist(uint8_t index)
{
    char cmd[30]="";
    sprintf(cmd, "at+read_userlist=%d\r\n", index);
    rak_uart_send(cmd,strlen(cmd));

}

/**
 *  brief.			write_userlist
 * @param[in]		index
 * @param[in]		len
 * @param[in]		data
 * @param[out]	none
 * @return			none
 */
void rak_write_userlist(uint8_t index, uint16_t len, uint8_t *data)
{
    char cmd[30]="";
    sprintf(cmd, "at+write_userlist=%d,%d,", index, len);
    rak_uart_send(cmd,strlen(cmd));
    rak_uart_send(data, len);
    rak_uart_send("\r\n", 2);

}

/**
 *  brief.			delete_userlist
 * @param[in]		index
 * @param[out]	none
 * @return			none
 */
void rak_delete_userlist(uint8_t index)
{
    char cmd[30]="";
    sprintf(cmd, "at+delete_userlist=%d\r\n", index);
    rak_uart_send(cmd,strlen(cmd));
}

/**
 *  brief.			enter uart_upgrade mode
 * @param[in]		none
 * @param[out]	none
 * @return			none
 */
void rak_enter_uart_upgrade(void)
{
    char cmd[30]="";
    sprintf(cmd, "at+upgrade\r\n");
    rak_uart_send(cmd,strlen(cmd));
}

