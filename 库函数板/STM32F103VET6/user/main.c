/****************************************************************************
* Copyright (C), 2013 �ܶ�Ƕ��ʽ������ www.ourstm.net
*
* �������� �ܶ���STM32������V2,2.1,V3,V5,MINI�ϵ���ͨ��           
* QQ: 9191274, ������sun68, Email: sun68@163.com 
* �Ա����̣�ourstm.taobao.com  
*
* �ļ���: main.c
* ���ݼ���:
*       ��ʾ USART2 �����յ���������0x0d 0x0aʱ�������յ������ݴ��ͳ�ȥ
                       V6��V3 V5�壩 V2��MINI�壩 LED1��˸��ʾ�����շ����ݡ� 
*
���壺	
	TXD2----- PA2-US2-TX
	RXD2----- PA3-US2-RX
	���ʣ�115200,n,8,1 
* �ļ���ʷ:
* �汾��  ����       ����    ˵��
* v0.2    2011-7-21 sun68  �������ļ�
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
* ��    �ƣ�void USART_Config(USART_TypeDef* USARTx)
* ��    �ܣ��ܶ��崮�ڳ�ʼ������
* ��ڲ�������
* ���ڲ�������
* ˵    ����
* ���÷������� 
****************************************************************************/
void USART_Config(USART_TypeDef* USARTx){
  USART_InitStructure.USART_BaudRate = 115200;						//����115200bps
  USART_InitStructure.USART_WordLength = USART_WordLength_8b;		//����λ8λ
  USART_InitStructure.USART_StopBits = USART_StopBits_1;			//ֹͣλ1λ
  USART_InitStructure.USART_Parity = USART_Parity_No;				//��У��λ
  USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;   //��Ӳ������
  USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;					//�շ�ģʽ

  /* Configure USARTx */
  USART_Init(USARTx, &USART_InitStructure);							//���ô��ڲ�������
 
  
  /* Enable USARTx Receive and Transmit interrupts */
  USART_ITConfig(USART2, USART_IT_RXNE, ENABLE);                    //ʹ�ܽ����ж�
  USART_ITConfig(USART2, USART_IT_TXE, ENABLE);						//ʹ�ܷ��ͻ�����ж�   

  /* Enable the USARTx */
  USART_Cmd(USART2, ENABLE);	
}
/****************************************************************************
* ��    �ƣ�int main(void)
* ��    �ܣ��ܶ���������
* ��ڲ�������
* ���ڲ�������
* ˵    ����
* ���÷������� 
****************************************************************************/
int main(void)
{

   uint8_t a=0;
  /* System Clocks Configuration */
  RCC_Configuration();											  //ϵͳʱ������
       
  /*Ƕ�������жϿ����� 
      ˵����USARTx��ռ���ȼ�����0�����1λ�� ���������ȼ�����0�����7λ�� */ 
  NVIC_Configuration();											  //�ж�Դ����

  /*�Կ���LEDָʾ�Ƶ�IO�ڽ����˳�ʼ�������˿�����Ϊ������������������ٶ�Ϊ50Mhz��PA2,PA2�˿ڸ���Ϊ����2��TX��RX��
  ������ĳ������ʱ������Ӧ�������ڵĶ˿ڵ�ʱ�ӽ���ʹ�ܡ������޷����óɹ��������õ��˶˿�B�� ���Ҫ������˿ڵ�ʱ��
  ����ʹ�ܣ�ͬʱ�����õ�����IO�ڹ����������ô��ڡ���˻�Ҫʹ��AFIO�����ù���IO��ʱ�ӡ�*/
  GPIO_Configuration();											  //�˿ڳ�ʼ��

  USART_Config(USART2);											  //����1��ʼ��
  
  USART_OUT(USART2,"****(C) COPYRIGHT 2013 �ܶ�Ƕ��ʽ���������� *******\r\n");    	  //�򴮿�1���Ϳ����ַ���
  USART_OUT(USART2,"*                                                 *\r\n");    	
  USART_OUT(USART2,"*         �ܶ���STM32������ USART2 ʵ��           *\r\n");  
  USART_OUT(USART2,"*                                                 *\r\n");    	  	
  USART_OUT(USART2,"*  ��HEXģʽ����һ�����ݣ���16����0d 0a��Ϊ����   *\r\n");    	
  USART_OUT(USART2,"*                                                 *\r\n");    	
  USART_OUT(USART2,"*  �ܶ�STM32��̳��www.ourstm.net                  *\r\n");   
  USART_OUT(USART2,"*                                                 *\r\n");    	 	
  USART_OUT(USART2,"***************************************************\r\n"); 
  USART_OUT(USART2,"\r\n"); 
  USART_OUT(USART2,"\r\n");    	
   while (1)
  {
	if(rec_f==1){												  //�ж��Ƿ��յ�һ֡��Ч����
		rec_f=0;
		USART_OUT(USART2,"\r\n�����͵���ϢΪ: \r\n");    
		USART_OUT(USART2,&TxBuffer1[0]);
		if(a==0) {GPIO_SetBits(GPIOB, GPIO_Pin_5); a=1;}          //LED1  V6��V3�壩 V2��MINI�壩 ������˸                
		else {GPIO_ResetBits(GPIOB, GPIO_Pin_5);a=0;  }
	}
  }
}

/****************************************************************************
* ��    �ƣ�void Delay(__IO uint32_t nCount)
* ��    �ܣ���ʱ����
* ��ڲ�������
* ���ڲ�������
* ˵    ����
* ���÷������� 
****************************************************************************/
void Delay(__IO uint32_t nCount)
{
  for(; nCount != 0; nCount--);
}

/****************************************************************************
* ��    �ƣ�void USART_OUT(USART_TypeDef* USARTx, uint8_t *Data,...)
* ��    �ܣ���ʽ�������������
* ��ڲ�����USARTx:  ָ������
			Data��   ��������
			...:     ��������
* ���ڲ�������
* ˵    ������ʽ�������������
        	"\r"	�س���	   USART_OUT(USARTx, "abcdefg\r")   
			"\n"	���з�	   USART_OUT(USARTx, "abcdefg\r\n")
			"%s"	�ַ���	   USART_OUT(USARTx, "�ַ����ǣ�%s","abcdefg")
			"%d"	ʮ����	   USART_OUT(USARTx, "a=%d",10)
* ���÷������� 
****************************************************************************/
void USART_OUT(USART_TypeDef* USARTx, uint8_t *Data,...){ 
	const char *s;
    int d;
    char buf[16];
    va_list ap;
    va_start(ap, Data);

	while(*Data!=0){				                          //�ж��Ƿ񵽴��ַ���������
		if(*Data==0x5c){									  //'\'
			switch (*++Data){
				case 'r':							          //�س���
					USART_SendData(USARTx, 0x0d);	   

					Data++;
					break;
				case 'n':							          //���з�
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
				case 's':										  //�ַ���
                	s = va_arg(ap, const char *);
                	for ( ; *s; s++) {
                    	USART_SendData(USARTx,*s);
						while(USART_GetFlagStatus(USARTx, USART_FLAG_TC)==RESET);
                	}
					Data++;
                	break;
            	case 'd':										  //ʮ����
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
		��������ת�ַ�������
        char *itoa(int value, char *string, int radix)
		radix=10 ��ʾ��10����	��ʮ���ƣ�ת�����Ϊ0;  

	    ����d=-379;
		ִ��	itoa(d, buf, 10); ��
		
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
* ��    �ƣ�void RCC_Configuration(void)
* ��    �ܣ�ϵͳʱ������Ϊ72MHZ�� ����ʱ������
* ��ڲ�������
* ���ڲ�������
* ˵    ����
* ���÷������� 
****************************************************************************/ 
void RCC_Configuration(void)
{
   SystemInit(); 
   RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOA | RCC_APB2Periph_GPIOB |RCC_APB2Periph_AFIO  , ENABLE); 
   RCC_APB1PeriphClockCmd( RCC_APB1Periph_USART2, ENABLE);  
}

/****************************************************************************
* ��    �ƣ�void GPIO_Configuration(void)
* ��    �ܣ�ͨ��IO������
* ��ڲ�������
* ���ڲ�������
* ˵    ����
* ���÷�����
****************************************************************************/  
void GPIO_Configuration(void)
{

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;				     //LED1����--PB5
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;			 //�������
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_Init(GPIOB, &GPIO_InitStructure);					 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;	         		 //USART2 TX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;    		 //�����������
  GPIO_Init(GPIOA, &GPIO_InitStructure);		    		 //A�˿� 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;	         	 	 //USART2 RX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;   	 //���ÿ�©����
  GPIO_Init(GPIOA, &GPIO_InitStructure);		         	 //A�˿� 
}

/****************************************************************************
* ��    �ƣ�void NVIC_Configuration(void)
* ��    �ܣ��ж�Դ����
* ��ڲ�������
* ���ڲ�������
* ˵    ����
* ���÷������� 
****************************************************************************/
void NVIC_Configuration(void)
{
  /*  �ṹ����*/
  NVIC_InitTypeDef NVIC_InitStructure;

  /* Configure the NVIC Preemption Priority Bits */  
  /* Configure one bit for preemption priority */
  /* ���ȼ��� ˵������ռ���ȼ����õ�λ�����������ȼ����õ�λ��   ��������1�� 7 */    
  NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);	  
  
  NVIC_InitStructure.NVIC_IRQChannel = USART2_IRQn;			     	//���ô���2�ж�
  NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;	     	//��ռ���ȼ� 0
  NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;				//�����ȼ�Ϊ0
  NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;					//ʹ��
  NVIC_Init(&NVIC_InitStructure);
}
/******************* (C) COPYRIGHT 2013 �ܶ�STM32 *****END OF FILE****/
