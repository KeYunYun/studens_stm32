#include "gpio.h"

void GPIO_Configuration(void)
{
	GPIO_InitTypeDef	GPIO_InitStructure;		//?????????
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;	         		 //USART1 TX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;    		 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		    		 //A?? 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;	         	 //USART1 RX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;   	 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		         	 //A?? 
	
	GPIO_InitStructure.GPIO_Pin=GPIO_Pin_5;
	GPIO_InitStructure.GPIO_Mode=GPIO_Mode_AF_PP;
	GPIO_InitStructure.GPIO_Speed=GPIO_Speed_50MHz;
	GPIO_Init(GPIOB,&GPIO_InitStructure);
	
	//�˿ڸ���
	GPIO_PinRemapConfig(GPIO_PartialRemap_TIM3,ENABLE);
} 

void GPIO_LCD_Config(void)
{
	GPIO_InitTypeDef	GPIO_InitStructure;
	  RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_GPIOB | RCC_APB2Periph_GPIOC |
                         RCC_APB2Periph_GPIOD | RCC_APB2Periph_GPIOE , ENABLE); 
	//PD13  PWD���Ʊ���
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_13; 			  //LCD�������
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
  GPIO_Init(GPIOD, &GPIO_InitStructure);
	//PE1   ��λ��
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_1 ; 	 		  //LCD��λ
  GPIO_Init(GPIOE, &GPIO_InitStructure);   	   	
  // ���ö˿�ΪFSMC�ӿ� FSMC-D0--D15
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_4 | GPIO_Pin_5 |
                                GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10 | GPIO_Pin_14 | 
                                GPIO_Pin_15;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
  GPIO_Init(GPIOD, &GPIO_InitStructure);   
   
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7 | GPIO_Pin_8 | GPIO_Pin_9 | GPIO_Pin_10 | 
                                GPIO_Pin_11 | GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14 | 
                                GPIO_Pin_15;
  GPIO_Init(GPIOE, &GPIO_InitStructure);    
  //FSMC NE1  LCDƬѡ
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_7; 
  GPIO_Init(GPIOD, &GPIO_InitStructure);
  
  //FSMC RS---LCDָ�� ָ��/����	�л�
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_11 ; 
  GPIO_Init(GPIOD, &GPIO_InitStructure); 	
  GPIO_SetBits(GPIOD, GPIO_Pin_13);			           //LCD�����
}
