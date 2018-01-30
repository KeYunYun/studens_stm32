#include "clock.h"
#include "led.h"
#include "daley.h"
#include "stm32f10x.h"
int main(void)
{
	Stm32_Clock_Init(9);
	RCC->APB2ENR|=1<<3;
 	GPIO_Init();
//	Led_Init();
		GPIOB->BRR=0x11111111;
	while(1)
	{
	 GPIO_led(1);
	 delay_ms(100);
	 GPIO_led(0); 
	delay_ms(100);
	 }



}
