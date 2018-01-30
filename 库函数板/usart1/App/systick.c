#include "systick.h"

/*void SYS_Tick_Configuration(void)
{
	  SysTick_CounterCmd(SysTick_Counter_Disable);		//ʧ�ܻ�����	
		SysTick_ITConfig(DISABLE);						//ʧ��systick�ж�
		SysTick_CLKSourceConfig(SysTick_CLKSource_HCLK_Div8);  //ʱ��8��Ƶ
	  SysTick_SetReload(9000);						 //������װ����ֵ ms *1000 s ϲ��
		SysTick_CounterCmd(SysTick_Counter_Enable);	 	 //ʹ�ܼ�����
		SysTick_ITConfig(ENABLE);						 //ʹ���ж�
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