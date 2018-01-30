#include "time.h"
#include "stm32f10x.h"
extern unsigned int CCR2_Val;
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


/****************************************************************************
* ?    ?:void time_ini(void)
* ?    ?:TIM3???
* ????:?
* ????:?
* ?    ?:
* ????:? 
****************************************************************************/ 
void time_ini(void){
  GPIO_InitTypeDef GPIO_InitStructure;
  RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE);

  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;						//PB5???TIM3???2
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
  GPIO_Init(GPIOB, &GPIO_InitStructure);
  /*TIM3????????	 ?TIM3????????,PB5?????TIM3_CH2*/
  GPIO_PinRemapConfig(GPIO_PartialRemap_TIM3 , ENABLE);			 
  				
   /*-------------------------------------------------------------------
  TIM3CLK=72MHz  ?????Prescaler=2 ???? ??????24MHz
  ???? ???????=TIM3_CCR2/(TIM_Period+1),????TIM_Pulse????	 
  ??/?????2 TIM3_CCR2= CCR2_Val 	     
  -------------------------------------------------------------------*/
  TIM3_TimeBaseStructure.TIM_Prescaler = 2;						    //????TIM3_PSC=3	 
  TIM3_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;		//????????? TIM3_CR1[4]=0
  TIM3_TimeBaseStructure.TIM_Period =24000;				            //????????TIM3_APR  ?????1KHz 		     
  TIM3_TimeBaseStructure.TIM_ClockDivision = 0x0;					//?????? TIM3_CR1[9:8]=00
  TIM3_TimeBaseStructure.TIM_RepetitionCounter = 0x0;

  TIM_TimeBaseInit(TIM3,&TIM3_TimeBaseStructure);					//?TIM3??????
  
  TIM3_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM2; 			    //PWM??2 TIM3_CCMR1[14:12]=111 ??????,
  																    //??TIMx_CNT<TIMx_CCR1???1?????,???????
  TIM3_OCInitStructure.TIM_OutputState = TIM_OutputState_Enable;    //??/??2????  OC2????????????PB5
  TIM3_OCInitStructure.TIM_Pulse = CCR2_Val; 					    //?????,??????????????
  TIM3_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_Low; 	    //????  ????? TIM3_CCER[5]=1;
         
  TIM_OC2Init(TIM3, &TIM3_OCInitStructure);
  TIM_OC2PreloadConfig(TIM3, TIM_OCPreload_Enable);
  TIM_Cmd(TIM3,ENABLE);											//?????3 TIM3_CR1[0]=1; 
}