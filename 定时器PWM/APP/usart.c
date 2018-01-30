#include "usart.h"


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

int fputc(int ch,FILE *f)
{
	USART_SendData(USART2,(u8)ch);
	while(USART_GetFlagStatus(USART1,USART_FLAG_TXE)==RESET);
	return ch;
}

void USART_Putc(char c)
{
	USART_SendData(USART2,c);
	while(USART_GetFlagStatus(USART2,USART_FLAG_TXE)==RESET);
}

void USART_putString(char *date)
{
	while(*date!='\0'){
		//date++;
		USART_SendData(USART2,*date++);
	  while(USART_GetFlagStatus(USART2,USART_FLAG_TXE)==RESET);
	}
	
}

