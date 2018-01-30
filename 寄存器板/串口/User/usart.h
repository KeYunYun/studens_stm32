#ifndef __USART_H
#define __USART_H
#include <stm32f10x_lib.h>
#include "stdio.h"	        	
extern u8 com;
void uart_init(u32 pclk2,u32 bound);
void Uart1_PutChar(u8 ch);
#endif	   
















