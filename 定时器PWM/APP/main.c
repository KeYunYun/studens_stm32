#include "systick.h"
#include "usart.h"
#include "rcc.h"
#include "gpio.h"
#include "nvic.h"
#include "time.h"
#include "fsmc.h"
#include "lcd.h"

int main(void)
{
	
	RCC_Configuration();
	NVIC_Config();
	USART_Configuration();
	GPIO_Configuration();
	GPIO_LCD_Config();
	FSMC_init();
	LCD_Init();	
	while(1) 
	{
			LCD_test();	
 }
}
