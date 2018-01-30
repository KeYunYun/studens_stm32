#include "time.h"
#include "stm32f10x.h"
void Time2_config(void)
{
  TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
  TIM_DeInit(TIM2);//初始化
  TIM_TimeBaseStructure.TIM_Period=9999;//装载值
  TIM_TimeBaseStructure.TIM_Prescaler=(7199);//预分频
  TIM_TimeBaseStructure.TIM_ClockDivision=TIM_CKD_DIV1;//分频
  TIM_TimeBaseStructure.TIM_CounterMode=TIM_CounterMode_Up;//????
	//TIM_TimeBaseStructure.TIM_RepetitionCounter = 0; 
  TIM_TimeBaseInit(TIM2,&TIM_TimeBaseStructure);
  TIM_ClearFlag(TIM2,TIM_FLAG_Update);//清除更新标志位
  TIM_ITConfig(TIM2,TIM_IT_Update,ENABLE);//更新事件
  TIM_Cmd(TIM2,ENABLE);//使能
}

void time_ini(void);
extern unsigned int CCR2_Val;
TIM_TimeBaseInitTypeDef  TIM3_TimeBaseStructure;
TIM_OCInitTypeDef  TIM3_OCInitStructure;
TIM_BDTRInitTypeDef TIM3_BDTRInitStructure;



