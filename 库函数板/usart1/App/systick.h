#ifndef __SYSTICK_H__
#define __SYSTICK_H__

#include "stm32f10x.h"

void SYS_Tick_Configuration(void);

void delay_ms(u16 nms);
void delay_us(u32 nus);
#endif