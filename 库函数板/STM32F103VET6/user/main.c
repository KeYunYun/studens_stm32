/****************************************************************************
* Copyright (C), 2013 奋斗嵌入式工作室 www.ourstm.net
*
* 本例程在 奋斗版STM32开发板V2,2.1,V3,V5,MINI上调试通过           
* QQ: 9191274, 旺旺：sun68, Email: sun68@163.com 
* 淘宝店铺：ourstm.taobao.com  
*
* 文件名: main.c
* 内容简述:
*       演示 USART2 当接收到结束符是0x0d 0x0a时，将接收到的数据传送出去
                       V6（V3 V5板） V2（MINI板） LED1闪烁表示正在收发数据。 
*
定义：	
	TXD2----- PA2-US2-TX
	RXD2----- PA3-US2-RX
	速率：115200,n,8,1 
* 文件历史:
* 版本号  日期       作者    说明
* v0.2    2011-7-21 sun68  创建该文件
*
*/
/* Includes ------------------------------------------------------------------*/
#include "stm32f10x.h"
#include "stm32f10x_usart.h"
#include "misc.h"
#include "stdarg.h"



/* Private variables ---------------------------------------------------------*/
USART_InitTypeDef USART_InitStructure;
uint8_t TxBuffer1[] = "USART Interrupt Example: This is USART2 DEMO";  
uint8_t RxBuffer1[],rec_f,tx_flag;
__IO uint8_t TxCounter1 = 0x00;
__IO uint8_t RxCounter1 = 0x00; 

uint32_t Rec_Len;


/* Private function prototypes -----------------------------------------------*/
void RCC_Configuration(void);
void GPIO_Configuration(void);
void NVIC_Configuration(void);

void Delay(__IO uint32_t nCount);
void USART_OUT(USART_TypeDef* USARTx, uint8_t *Data,...);
char *itoa(int value, char *string, int radix);
void USART_Config(USART_TypeDef* USARTx);


GPIO_InitTypeDef GPIO_InitStructure;
USART_InitTypeDef USART_InitStruct;
USART_ClockInitTypeDef USART_ClockInitStruct;

/****************************************************************************
* 名    称：void USART_Config(USART_TypeDef* USARTx)
* 功    能：奋斗板串口初始化函数
* 入口参数：无
* 出口参数：无
* 说    明：
* 调用方法：无 
****************************************************************************/
void USART_Config(USART_TypeDef* USARTx){
  USART_InitStructure.USART_BaudRate = 115200;						//速率115200bps
  USART_InitStructure.USART_WordLength = USART_WordLength_8b;		//数据位8位
  USART_InitStructure.USART_StopBits = USART_StopBits_1;			//停止位1位
  USART_InitStructure.USART_Parity = USART_Parity_No;				//无校验位
  USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;   //无硬件流控
  USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;					//收发模式

  /* Configure USARTx */
  USART_Init(USARTx, &USART_InitStructure);							//配置串口参数函数
 
  
  /* Enable USARTx Receive and Transmit interrupts */
  USART_ITConfig(USART2, USART_IT_RXNE, ENABLE);                    //使能接收中断
  USART_ITConfig(USART2, USART_IT_TXE, ENABLE);						//使能发送缓冲空中断   

  /* Enable the USARTx */
  USART_Cmd(USART2, ENABLE);	
}
/****************************************************************************
* 名    称：int main(void)
* 功    能：奋斗板主函数
* 入口参数：无
* 出口参数：无
* 说    明：
* 调用方法：无 
****************************************************************************/
int main(void)
{

   uint8_t a=0;
  /* System Clocks Configuration */
  RCC_Configuration();											  //系统时钟设置
       
  /*嵌套向量中断控制器 
      说明了USARTx抢占优先级级别0（最多1位） ，和子优先级级别0（最多7位） */ 
  NVIC_Configuration();											  //中断源配置

  /*对控制LED指示灯的IO口进行了初始化，将端口配置为推挽上拉输出，口线速度为50Mhz。PA2,PA2端口复用为串口2的TX，RX。
  在配置某个口线时，首先应对它所在的端口的时钟进行使能。否则无法配置成功，由于用到了端口B， 因此要对这个端口的时钟
  进行使能，同时由于用到复用IO口功能用于配置串口。因此还要使能AFIO（复用功能IO）时钟。*/
  GPIO_Configuration();											  //端口初始化

  USART_Config(USART2);											  //串口1初始化
  
  USART_OUT(USART2,"****(C) COPYRIGHT 2013 奋斗嵌入式开发工作室 *******\r\n");    	  //向串口1发送开机字符。
  USART_OUT(USART2,"*                                                 *\r\n");    	
  USART_OUT(USART2,"*         奋斗版STM32开发板 USART2 实验           *\r\n");  
  USART_OUT(USART2,"*                                                 *\r\n");    	  	
  USART_OUT(USART2,"*  以HEX模式输入一串数据，以16进制0d 0a作为结束   *\r\n");    	
  USART_OUT(USART2,"*                                                 *\r\n");    	
  USART_OUT(USART2,"*  奋斗STM32论坛：www.ourstm.net                  *\r\n");   
  USART_OUT(USART2,"*                                                 *\r\n");    	 	
  USART_OUT(USART2,"***************************************************\r\n"); 
  USART_OUT(USART2,"\r\n"); 
  USART_OUT(USART2,"\r\n");    	
   while (1)
  {
	if(rec_f==1){												  //判断是否收到一帧有效数据
		rec_f=0;
		USART_OUT(USART2,"\r\n您发送的信息为: \r\n");    
		USART_OUT(USART2,&TxBuffer1[0]);
		if(a==0) {GPIO_SetBits(GPIOB, GPIO_Pin_5); a=1;}          //LED1  V6（V3板） V2（MINI板） 明暗闪烁                
		else {GPIO_ResetBits(GPIOB, GPIO_Pin_5);a=0;  }
	}
  }
}

/****************************************************************************
* 名    称：void Delay(__IO uint32_t nCount)
* 功    能：延时函数
* 入口参数：无
* 出口参数：无
* 说    明：
* 调用方法：无 
****************************************************************************/
void Delay(__IO uint32_t nCount)
{
  for(; nCount != 0; nCount--);
}

/****************************************************************************
* 名    称：void USART_OUT(USART_TypeDef* USARTx, uint8_t *Data,...)
* 功    能：格式化串口输出函数
* 入口参数：USARTx:  指定串口
			Data：   发送数组
			...:     不定参数
* 出口参数：无
* 说    明：格式化串口输出函数
        	"\r"	回车符	   USART_OUT(USARTx, "abcdefg\r")   
			"\n"	换行符	   USART_OUT(USARTx, "abcdefg\r\n")
			"%s"	字符串	   USART_OUT(USARTx, "字符串是：%s","abcdefg")
			"%d"	十进制	   USART_OUT(USARTx, "a=%d",10)
* 调用方法：无 
****************************************************************************/
void USART_OUT(USART_TypeDef* USARTx, uint8_t *Data,...){ 
	const char *s;
    int d;
    char buf[16];
    va_list ap;
    va_start(ap, Data);

	while(*Data!=0){				                          //判断是否到达字符串结束符
		if(*Data==0x5c){									  //'\'
			switch (*++Data){
				case 'r':							          //回车符
					USART_SendData(USARTx, 0x0d);	   

					Data++;
					break;
				case 'n':							          //换行符
					USART_SendData(USARTx, 0x0a);	
					Data++;
					break;
				
				default:
					Data++;
				    break;
			}
			
			 
		}
		else if(*Data=='%'){									  //
			switch (*++Data){				
				case 's':										  //字符串
                	s = va_arg(ap, const char *);
                	for ( ; *s; s++) {
                    	USART_SendData(USARTx,*s);
						while(USART_GetFlagStatus(USARTx, USART_FLAG_TC)==RESET);
                	}
					Data++;
                	break;
            	case 'd':										  //十进制
                	d = va_arg(ap, int);
                	itoa(d, buf, 10);
                	for (s = buf; *s; s++) {
                    	USART_SendData(USARTx,*s);
						while(USART_GetFlagStatus(USARTx, USART_FLAG_TC)==RESET);
                	}
					Data++;
                	break;
				default:
					Data++;
				    break;
			}		 
		}
		else USART_SendData(USARTx, *Data++);
		while(USART_GetFlagStatus(USARTx, USART_FLAG_TC)==RESET);
	}
}

/******************************************************
		整形数据转字符串函数
        char *itoa(int value, char *string, int radix)
		radix=10 标示是10进制	非十进制，转换结果为0;  

	    例：d=-379;
		执行	itoa(d, buf, 10); 后
		
		buf="-379"							   			  
**********************************************************/
char *itoa(int value, char *string, int radix)
{
    int     i, d;
    int     flag = 0;
    char    *ptr = string;

    /* This implementation only works for decimal numbers. */
    if (radix != 10)
    {
        *ptr = 0;
        return string;
    }

    if (!value)
    {
        *ptr++ = 0x30;
        *ptr = 0;
        return string;
    }

    /* if this is a negative value insert the minus sign. */
    if (value < 0)
    {
        *ptr++ = '-';

        /* Make the value positive. */
        value *= -1;
    }

    for (i = 10000; i > 0; i /= 10)
    {
        d = value / i;

        if (d || flag)
        {
            *ptr++ = (char)(d + 0x30);
            value -= (d * i);
            flag = 1;
        }
    }

    /* Null terminate the string. */
    *ptr = 0;

    return string;

} /* NCL_Itoa */

/****************************************************************************
* 名    称：void RCC_Configuration(void)
* 功    能：系统时钟配置为72MHZ， 外设时钟配置
* 入口参数：无
* 出口参数：无
* 说    明：
* 调用方法：无 
****************************************************************************/ 
void RCC_Configuration(void)
{
   SystemInit(); 
   RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOA | RCC_APB2Periph_GPIOB |RCC_APB2Periph_AFIO  , ENABLE); 
   RCC_APB1PeriphClockCmd( RCC_APB1Periph_USART2, ENABLE);  
}

/****************************************************************************
* 名    称：void GPIO_Configuration(void)
* 功    能：通用IO口配置
* 入口参数：无
* 出口参数：无
* 说    明：
* 调用方法：
****************************************************************************/  
void GPIO_Configuration(void)
{

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;				     //LED1控制--PB5
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;			 //推挽输出
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_Init(GPIOB, &GPIO_InitStructure);					 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;	         		 //USART2 TX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;    		 //复用推挽输出
  GPIO_Init(GPIOA, &GPIO_InitStructure);		    		 //A端口 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;	         	 	 //USART2 RX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;   	 //复用开漏输入
  GPIO_Init(GPIOA, &GPIO_InitStructure);		         	 //A端口 
}

/****************************************************************************
* 名    称：void NVIC_Configuration(void)
* 功    能：中断源配置
* 入口参数：无
* 出口参数：无
* 说    明：
* 调用方法：无 
****************************************************************************/
void NVIC_Configuration(void)
{
  /*  结构声明*/
  NVIC_InitTypeDef NVIC_InitStructure;

  /* Configure the NVIC Preemption Priority Bits */  
  /* Configure one bit for preemption priority */
  /* 优先级组 说明了抢占优先级所用的位数，和子优先级所用的位数   在这里是1， 7 */    
  NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);	  
  
  NVIC_InitStructure.NVIC_IRQChannel = USART2_IRQn;			     	//设置串口2中断
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;	     	//抢占优先级 0
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;				//子优先级为0
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;					//使能
  NVIC_Init(&NVIC_InitStructure);
}
/******************* (C) COPYRIGHT 2013 奋斗STM32 *****END OF FILE****/
