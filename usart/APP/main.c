#include "stm32f10x.h"
#include  <stdio.h>

void delay_ms(u16 nms)
{
	 u32 temp;
	 SysTick->LOAD = 9000*nms;
	 SysTick->VAL=0X00;
	 SysTick->CTRL=0X01;
	 do
	 {
	  temp=SysTick->CTRL;
	 }while((temp&0x01)&&(!(temp&(1<<16))));
	 SysTick->CTRL=0x00; 
	 SysTick->VAL =0X00; 
}
void delay_us(u32 nus)
{
	 u32 temp;
	 SysTick->LOAD = 9*nus;
	 SysTick->VAL=0X00;
	 SysTick->CTRL=0X01;
	 do
	 {
	  temp=SysTick->CTRL;
	 }while((temp&0x01)&&(!(temp&(1<<16))));
	 
	 SysTick->CTRL=0x00; 
	 SysTick->VAL =0X00;
}

int fputc(int ch,FILE *f)
{
	USART_SendData(USART1,(u8)ch);
	while(USART_GetFlagStatus(USART1,USART_FLAG_TXE)==RESET);
	return ch;
}

void RCC_Configuration(void)
{
   SystemInit(); 
   RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA |RCC_APB2Periph_AFIO  , ENABLE);  
	 RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART2|RCC_APB1Periph_TIM2,ENABLE);
}

void GPIO_Configuration(void)
{
	GPIO_InitTypeDef	GPIO_InitStructure;		//?????????
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;	         		 //USART1 TX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;    		 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		    		 //A?? 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;	         	 //USART1 RX
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
	USART_Init(USART2,&USART_InitStructure);		//???USART3????

	//USART_ITConfig(USART3,USART_IT_RXNE,ENABLE);
	//USART_ITConfig(USART2,USART_IT_TXE,ENABLE);
	USART_ITConfig(USART2,USART_IT_RXNE,ENABLE);
	
	USART_Cmd(USART2,ENABLE);	//??USART3??;
	
}

void NVIC_Config(void)
{
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
		
	NVIC_InitTypeDef NVIC_initestruct;
	NVIC_initestruct.NVIC_IRQChannel=USART2_IRQn;
	NVIC_initestruct.NVIC_IRQChannelPreemptionPriority=0;
	NVIC_initestruct.NVIC_IRQChannelCmd=ENABLE;
	NVIC_Init(&NVIC_initestruct);
}

void USART_Putc(char c)
{
	USART_SendData(USART2,c);
	while(USART_GetFlagStatus(USART2,USART_FLAG_TXE)==RESET);
}


void USART2_IRQHandler(void)    
{   

	uint16_t ch;
	uint8_t i=0;
    if(USART_GetITStatus(USART2,USART_IT_RXNE) != RESET) //????  
    {    
		//	USART_Putc('q');
	
     USART_ClearITPendingBit(USART2, USART_IT_RXNE);
			
		  ch = USART_ReceiveData(USART2);     //????
      USART_Putc(ch);
			 
			
		}
	//	if(USART_GetITStatus(USART2, USART_IT_TXE) != RESET){  
       // 
         //  USART_ITConfig(USART1, USART_IT_TXE, DISABLE);   
       //  }
  
}  
    


int main(void)
{
	
	RCC_Configuration();
	NVIC_Config();
	USART_Configuration();
	GPIO_Configuration();
	

	while(1)
	{
	//USART2->SR |=0x80;
	//	USART2->DR|='s';
		USART_Putc('c');
		delay_ms(1000);


			
 }
}