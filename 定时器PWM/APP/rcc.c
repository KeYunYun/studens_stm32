#include "rcc.h"

void RCC_Configuration(void)
{
   SystemInit(); 
   RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA |RCC_APB2Periph_AFIO  , ENABLE);  
	 RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART2|RCC_APB1Periph_TIM2|RCC_APB1Periph_TIM13,ENABLE);
}


