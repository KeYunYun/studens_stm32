#include "systick.h"

/*void SYS_Tick_Configuration(void)
{
	  SysTick_CounterCmd(SysTick_Counter_Disable);		//失能基数器	
		SysTick_ITConfig(DISABLE);						//失能systick中断
		SysTick_CLKSourceConfig(SysTick_CLKSource_HCLK_Div8);  //时钟8分频
	  SysTick_SetReload(9000);						 //设置重装置数值 ms *1000 s 喜欢
		SysTick_CounterCmd(SysTick_Counter_Enable);	 	 //使能计算器
		SysTick_ITConfig(ENABLE);						 //使能中断
} */

void delay_ms(u16 nms)
{
	 u32 temp;
	 SysTick->LOAD = 9000*nms;
	 SysTick->VAL=0X00;
	 SysTick->CTRL=0X01;
	 do
	 {
	  temp=SysTick->CTRL;
	 }while((temp&0x01)&&(!(temp&(1<<16))));
	 SysTick->CTRL=0x00; 
	 SysTick->VAL =0X00; 
}
void delay_us(u32 nus)
{
	 u32 temp;
	 SysTick->LOAD = 9*nus;
	 SysTick->VAL=0X00;
	 SysTick->CTRL=0X01;
	 do
	 {
	  temp=SysTick->CTRL;
	 }while((temp&0x01)&&(!(temp&(1<<16))));
	 
	 SysTick->CTRL=0x00; 
	 SysTick->VAL =0X00;
}