#include"led.h"
void GPIO_Init(void)
{
	GPIOB->CRH&=0XFFFFFFF0;
	GPIOB->CRH|=0X33333333;
	GPIOB->BRR=0x11111111;
	GPIOB->ODR=0x10;
	
}
void GPIO_led(u32 state)
{
	if(state==1)
	{
		GPIOB->BSRR=0x00000100;
	}
	else
	{
	GPIOB->BRR=0x00000100;
	}
}
