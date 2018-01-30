#ifndef __USART_H
#define __USART_H
#include "stm32f10x.h"
#include  <stdio.h>

void USART_Configuration(void);
void USART_Putc(char c);
int fputc(int ch,FILE *f);
void USART_putString(char *date);

#endif

