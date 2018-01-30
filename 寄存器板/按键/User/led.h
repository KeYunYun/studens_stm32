#ifndef __LED_H
#define __LED_H
#include<stm32f10x.h>
void GPIO_Init(void);
void GPIO_led(u32 state);
void key_Init(void);
#endif
