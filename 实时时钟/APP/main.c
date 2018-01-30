#include "systick.h"
#include "usart.h"
#include "rcc.h"
#include "gpio.h"
#include "nvic.h"
#include "time.h"

int main(void)
{
	
	RCC_Configuration();
	NVIC_Config();
	USART_Configuration();
	GPIO_Configuration();
	Time2_config();

	while(1)
	{
	//USART2->SR |=0x80;
	//	USART2->DR|='s';
		USART_Putc('c');
	 //delay1ms(1000);
		//delay_ms(1000);


			
 }
}
