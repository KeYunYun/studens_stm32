
#include "stm32f10x.h"




void USART_Config(){
	USART_InitTypeDef USART_InitStruct;
  USART_InitStruct.USART_BaudRate = 115200;						//??115200bps
  USART_InitStruct.USART_WordLength = USART_WordLength_8b;		//???8?
  USART_InitStruct.USART_StopBits = USART_StopBits_1;			//???1?
  USART_InitStruct.USART_Parity = USART_Parity_No;				//????
  USART_InitStruct.USART_HardwareFlowControl = USART_HardwareFlowControl_None;   //?????
  USART_InitStruct.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;					//????

  USART_Init(USART2, &USART_InitStruct);							//????????
 
  

  USART_Cmd(USART2, ENABLE);	
}





void RCC_Configuration(void)
{
   SystemInit(); 
   RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOA | RCC_APB2Periph_GPIOB |RCC_APB2Periph_AFIO  , ENABLE); 
   RCC_APB1PeriphClockCmd( RCC_APB1Periph_USART2, ENABLE);  
}


void GPIO_Configuration(void)
{
  GPIO_InitTypeDef GPIO_InitStructure;
	
		 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;	         		 //USART2 TX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;    		 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		    		 //A?? 

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_3;	 
	//USART2 RX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;   	 //??????
  GPIO_Init(GPIOA, &GPIO_InitStructure);		         	 //A?? 
}

int main(void)
{

   uint8_t a=0;

  RCC_Configuration();											  //??????
       

  GPIO_Configuration();											  //?????

  USART_Config();											  //??1???
  
   	
   while (1)
  {
		USART_SendData(USART2, 0x26);
	
  }
}
