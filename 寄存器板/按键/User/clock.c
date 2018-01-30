#include "clock.h"

void Stm32_Clock_Init(unsigned char PLL)
{
	unsigned char temp=0;
	/*复位RCC_CR寄存器*/
	RCC->CR = 0x00000083;
	RCC->CR|= 0x00010000;//设置第16位
	while(!(RCC->CR>>17));
	RCC->CFGR=0x00000400;
	PLL-=2;
	RCC->CFGR|=PLL<<18;
	RCC->CFGR|=1<<16;
	FLASH->ACR|=0x32;
	RCC->CR|=0x01000000;
	while(!(RCC->CR>>25));
	RCC->CFGR|=0x00000002;
	while(temp!=0x02)
	{
		temp=RCC->CFGR>>2;
		temp&=0x03;
	}



}
