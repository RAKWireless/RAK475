#ifndef _RAKGLOBAL_H_
#define _RAKGLOBAL_H_

#include <stdint.h>

/**
 *  Type Definitions
 */
typedef unsigned char	uint8;
typedef unsigned short	uint16;
typedef unsigned long	uint32;
typedef signed char	    int8;
typedef signed short	int16;
typedef signed long	     int32;

/**
 *  socketID Definitions
 */
typedef enum Socket {
    SOCKETA_ID  =0 ,
    SOCKETB_ID
} Socket_ID;

/**
 *  Error Definitions
 */
#define    RAK_SUCCESS              0
#define    RAK_FAIL                -1
#define    RAK_CFG_ERROR           -2

/**
 *  Interface Definition
 */
#define    RXBUFSIZE 1100

#define    RESET_PORT               P2
#define    RESET_PIN                4
#define    RESET_PORT_PIN           P24	   //Module RESET pin

#define    RAK_DEBUG_PORT             P2
#define    RAK_DEBUG_PIN              5
#define    RAK_DEBUG_PORT_PIN         P25	   //Module DEBUG pin


/**
 *  socket Definition
 */
#define    RAK_RECIEVE_DATA_CMD             "at+recv_data="
#define    SOCKETA_HDER			    "S0"
#define    SOCKETB_HDER			    "S1"


/**
 *  timeout Definition
 */
#define RAK_TICKS_PER_MISECOND      100000 					 //@MCU-M0  12MHz  100ms
#define RAK_INC_TIMER               rak_Timer++
#define RAK_RESET_TIMER             rak_Timer=0

#define RAK_200MS_TIMEOUT           2 * RAK_TICKS_PER_MISECOND
#define RAK_3S_TIMEOUT           		30 * RAK_TICKS_PER_MISECOND
#define RAK_QRYFWTIMEOUT			    	1 * RAK_TICKS_PER_MISECOND
#define RAK_QMACTIMEOUT			    		1 * RAK_TICKS_PER_MISECOND
#define RAK_WRCFGTIMEOUT						5 * RAK_TICKS_PER_MISECOND
#define RAK_RDCFGTIMEOUT						5 * RAK_TICKS_PER_MISECOND
#define RAK_QCONSATUSTIMEOUT        10 * RAK_TICKS_PER_MISECOND
#define RAK_QIPCFGTIMEOUT           2 * RAK_TICKS_PER_MISECOND
#define RAK_WPSTIMEOUT              1 * RAK_TICKS_PER_MISECOND
#define RAK_EASYCFGTIMEOUT          1 * RAK_TICKS_PER_MISECOND
#define RAK_SCANTIMEOUT							50 * RAK_TICKS_PER_MISECOND
#define RAK_GETSCANTIMEOUT					1 * RAK_TICKS_PER_MISECOND
#define RAK_QRYNETINFOTIMEOUT				5 * RAK_TICKS_PER_MISECOND

/**
 * Device Parameters
 */
#define RAK_FRAME_CMD_RSP_LEN       10
#define RAK_MAX_PAYLOAD_SIZE				1200      // maximum recieve data payload size
#define RAK_MAX_PAYLOAD_SEND_SIZE		800       // maximum recieve data payload size
#define RAK_AP_SCANNED_MAX		      20	       // maximum number of scanned acces points

#define RAK_MAX_DOMAIN_NAME_LEN 		42
#define RAK_SSID_LEN			        	33	     // maximum length of SSID
#define RAK_BSSID_LEN			        	6	     // maximum length of SSID
#define RAK_IP_ADD_LEN              4
#define RAK_MAC_ADD_LEN             6

#define RAK_PSK_LEN									33


typedef struct {
    uint8					ssid[RAK_SSID_LEN];				// 33-byte ssid of scanned access point
    uint8					bssid[RAK_BSSID_LEN];			// 32-byte bssid of scanned access point
    uint8					rfChannel;								// rf channel to us, 0=scan for all
    uint8					rssiVal;									// absolute value of RSSI
    uint8					securityMode;							// security mode
//	uint8	 				end[2];
} rak_getscanInfo;

typedef  union {
    struct {
        uint8           rspCode[2];  			   //0= success	   !0= Failure
        uint8				    scanCount;				// uint8, number of access points found
        uint8	 					end[2];
    } scanOkframe;
    struct {
        uint8           rspCode[5];                    		// command code
        uint8           ErrorCode;
        uint8	 					end[2];
    } scanErrorframe;
    uint8				      scanFrameRcv[8]  ;			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_scanResponse;

typedef  union {
    struct {
        uint8           rspCode[2];
        rak_getscanInfo strScanInfo[RAK_AP_SCANNED_MAX];	// 32 maximum responses from scan command
        uint8	 					end[2];
    } getOkframe;
    struct {
        uint8           rspCode[5];                    		// command code
        uint8           ErrorCode;
        uint8	 					end[2];
    } getErrorframe;
    uint8				      getscanFrameRcv[RAK_AP_SCANNED_MAX*42+4] ;			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_getscanResponse;


typedef struct {
    uint8                   rspCode[2];  	 //0=connected  -2=no connect
} rak_qryconResponse;

/*typedef struct {
	uint8                   rspCode; 			        //0= success  -3,-4,-5,-6,-7,-8=error
	uint8					ssid[RAK_SSID_LEN];			//33-byte ssid of wps connect access point
	uint8					securityMode;				//security mode
	uint8					psk[RAK_PSK_LEN	];			    //65Byte
	uint8                   end[2];						//\r\n
} rak_wpsconnectResponse; */


typedef  union {

    struct {
        uint8           rspCode[2];  	 //0=connected  -2=no connect
        uint8           rssi;
        uint8	 			    end[2];
    } qryrssiOkframe;
    uint8				       qryrssiFrameRcv[5]  ;			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_qryrssiResponse;

typedef  union {

    struct {
        uint8           rspCode[2];  	 //0=connected  -2=no connect
        uint8           status;
        uint8	 			    end[2];
    } qryconstatusframe;
    uint8				       qryconstatusFrameRcv[5]  ;			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_qryconstatusResponse;

typedef  union {

    struct {
        uint8           rspCode[2];  			   //0= success	   !0= Failure
        uint8				    ipAddr[4];				// Configured IP address
        uint8				    netMask[4];				// Configured netmask
        uint8				    gateWay[4];				// Configured default gateway
        uint8				    dns1[4];				// dns1
        uint8				    dns2[4];				// dns2
        uint8	 					end[2];
    } qryipconfigframe;

    uint8				      qryipconfigFrameRcv[24];			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_qryipconfigResponse;



typedef struct {
    uint8               rspCode[2]; 			   //0= success	   !0= Failure
} rak_pingResponse;


typedef  union {

    struct {
        uint8           recvheader[13]; 		   // at+recv_data=
        uint8				    socketID;
        uint8					  destPort[2];
        uint8					  destIp[4];
        uint8					  recDataLen[2];
        uint8                       recvdataBuf[RAK_MAX_PAYLOAD_SIZE];
        uint8	 					end[2];
    } recvdata;
    uint8				        socketFrameRcv[RAK_MAX_PAYLOAD_SIZE+24]  ;			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_recvdataFrame;


typedef  union {

    struct {
        uint8           rspCode[2];
        uint8						hostFwversion[8];				// uint8[10], firmware version text string, 1.0.6.0
    } qryframe;
    uint8				      	qryFwversionFrameRcv[10]  ;			// uint8, socket descriptor, like a file handle, usually 0x00
} rak_qryFwversionFrameRcv;



typedef union {

    rak_scanResponse          		scanResponse;
    rak_getscanResponse          	getscanResponse;
//	rak_wpsconnectResponse          wpsconnectResponse;
    rak_qryconstatusResponse		qryconstatusResponse;
    rak_qryrssiResponse             qryrssiResponse;
    rak_qryipconfigResponse         qryipconfigResponse;
    rak_pingResponse                pingResponse;
    rak_recvdataFrame          		recvdataFrame;
//	rak_resetResponse               resetResponse;
    rak_qryFwversionFrameRcv  		qryFwversionFrameRcv;
    uint8					        uCmdRspBuf[RAK_FRAME_CMD_RSP_LEN + RAK_MAX_PAYLOAD_SIZE];

} rak_uCmdRsp;

int8 * rak_bytes4ToAsciiDotAddr(uint8 *hexAddr,uint8 *strBuf);
void rak_asciiDotAddressTo4Bytes(uint8 *hexAddr, int8 *asciiDotAddress,  uint8 length);

#endif

