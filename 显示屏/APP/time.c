#include "time.h"
#include "stm32f10x.h"
void Time2_config(void)
{
  TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
  TIM_DeInit(TIM2);//��ʼ��
  TIM_TimeBaseStructure.TIM_Period=9999;//װ��ֵ
  TIM_TimeBaseStructure.TIM_Prescaler=(7199);//Ԥ��Ƶ
  TIM_TimeBaseStructure.TIM_ClockDivision=TIM_CKD_DIV1;//��Ƶ
  TIM_TimeBaseStructure.TIM_CounterMode=TIM_CounterMode_Up;//????
	//TIM_TimeBaseStructure.TIM_RepetitionCounter = 0; 
  TIM_TimeBaseInit(TIM2,&TIM_TimeBaseStructure);
  TIM_ClearFlag(TIM2,TIM_FLAG_Update);//������±�־λ
  TIM_ITConfig(TIM2,TIM_IT_Update,ENABLE);//�����¼�
  TIM_Cmd(TIM2,ENABLE);//ʹ��
}

void time_ini(void);
extern unsigned int CCR2_Val;
TIM_TimeBaseInitTypeDef  TIM3_TimeBaseStructure;
TIM_OCInitTypeDef  TIM3_OCInitStructure;
TIM_BDTRInitTypeDef TIM3_BDTRInitStructure;



