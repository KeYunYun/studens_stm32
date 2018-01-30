#include "stm32f10x.h"
#include  <stdio.h>
#include "systick.h"

int fputc(int ch,FILE *f)
{
	USART_SendData(USART1,(u8)ch);
	while(USART_GetFlagStatus(USART1,USART_FLAG_TXE)==RESET);
	return ch;
}

void RCC_Configuration(void)
{
   SystemInit(); 
   RCC_APB2PeriphClockCmd( RCC_APB2Periph_USART1 |RCC_APB2Periph_GPIOA | RCC_APB2Periph_GPIOB |RCC_APB2Periph_AFIO  , ENABLE);  
}

void GPIO_Configuration(void)
{
	GPIO_InitTypeDef	GPIO_InitStructure;		//?????????
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_9;	         		 //USART1 TX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;    		 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		    		 //A?? 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10;	         	 //USART1 RX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;   	 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		         	 //A?? 
} 

void USART_Configuration(void)
{

	USART_InitTypeDef	USART_InitStructure; 	

	USART_InitStructure.USART_BaudRate = 115200;		//??USART??????
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;	//8????
	USART_InitStructure.USART_StopBits = USART_StopBits_1;	//???????????????
	USART_InitStructure.USART_Parity = USART_Parity_No;	//?????
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;//??????
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;	//???????(??????)
	USART_Init(USART1,&USART_InitStructure);		//???USART3????

	//USART_ITConfig(USART3,USART_IT_RXNE,ENABLE);
	//USART_ITConfig(USART1,USART_IT_TXE,ENABLE);
	//USART_ITConfig(USART1,USART_IT_RXNE,ENABLE);
	
	//USART_Cmd(USART1,ENABLE);	//??USART3??;
	USART3->CR3 |=0x08;
}

void NVIC_Config(void)
{
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
		
	NVIC_InitTypeDef NVIC_initestruct;
	NVIC_initestruct.NVIC_IRQChannel=USART1_IRQn;
	NVIC_initestruct.NVIC_IRQChannelPreemptionPriority=0;
	NVIC_initestruct.NVIC_IRQChannelCmd=ENABLE;
	NVIC_Init(&NVIC_initestruct);
}

void USART_Putc(char c)
{
	USART_SendData(USART1,c);
	while(USART_GetFlagStatus(USART2,USART_FLAG_TXE)==RESET);
}

/*
void USART1_IRQHandler(void)    
{   

	uint8_t ch;
	uint8_t i=0;
    if(USART_GetITStatus(USART1,USART_IT_RXNE) != RESET) //????  
    {    
      USART_ClearITPendingBit(USART1, USART_IT_RXNE);
			
		  ch = USART_ReceiveData(USART1);     //????
          printf( "%c", ch );
			
			
		}
		if(USART_GetITStatus(USART1, USART_IT_TXE) != RESET){  
       // 
         //  USART_ITConfig(USART1, USART_IT_TXE, DISABLE);   
         }
  
}  
     
*/

int main(void)
{
	

			
}