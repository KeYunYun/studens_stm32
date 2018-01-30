#ifndef __NVIC_H
#define __NVIC_H
#include "stm32f10x.h"


void NVIC_Config(void);
void USART2_IRQHandler(void)  ;
void delay1ms(u32 nTimer);
void TIM2_IRQHandler(void);

#endif

