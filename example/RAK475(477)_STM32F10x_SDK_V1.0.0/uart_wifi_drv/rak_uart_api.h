#ifndef _RAK_UART_API_H_
#define _RAK_UART_API_H_
#include "rak_global.h"

/*******************  correlation function ******************/

int8 rak_open_cmdMode(void);
void rak_easy_txrxMode(void);
void rak_query_fwversion(void);
void rak_query_macaddr(void);
void rak_read_userConfig(void);
void rak_write_userConfig(void);
void rak_read_restoreConfig(void);
void rak_write_restoreConfig(void);
void rak_copy_uConfig(void);
void rak_query_constatus(void);
void rak_query_ipconifg(void);
void rak_query_tcpstatus(uint8_t sock_id);
void rak_query_rssi(void);
void rak_scan_ap(uint8_t channel, char *ssid);
void rak_get_scan(uint8_t ap_num);

void rak_easy_config(void);
void rak_wps_config(void);
void rak_reset(void);
void rak_restore(void);

void rak_query_netinfo(void);
void rak_send_data(uint8 sock_fd, uint16 dest_port, uint32 dest_ip, uint16 send_len, uint8 *buf);
void rak_nvm_write(uint32_t addr, uint16_t len, char *data);
void rak_nvm_read(uint32_t addr, uint16_t len);
void rak_set_cert(uint8_t cert_type, uint16_t len, uint8_t *buf);
void rak_read_userlist_num(void);
void rak_read_userlist(uint8_t index);
void rak_write_userlist(uint8_t index, uint16_t len, uint8_t *data);
void rak_delete_userlist(uint8_t index);
void rak_enter_uart_upgrade(void);

#endif

