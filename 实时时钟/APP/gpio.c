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
	
	//¶Ë¿Ú¸´ÓÃ
	GPIO_PinRemapConfig(GPIO_PartialRemap_TIM3,ENABLE);
} 

