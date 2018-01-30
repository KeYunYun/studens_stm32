#include "nvic.h"
#include "usart.h"


void NVIC_Config(void)
{
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_0);
	//usart中断
	NVIC_InitTypeDef NVIC_initestruct;
	NVIC_initestruct.NVIC_IRQChannel=USART2_IRQn;
	NVIC_initestruct.NVIC_IRQChannelPreemptionPriority=1;
	NVIC_initestruct.NVIC_IRQChannelCmd=ENABLE;
	NVIC_Init(&NVIC_initestruct);
	//tim2中断
	NVIC_InitTypeDef NVIC_initestruct1;
	NVIC_initestruct1.NVIC_IRQChannel=TIM2_IRQn;
	NVIC_initestruct1.NVIC_IRQChannelPreemptionPriority=0;
	NVIC_initestruct1.NVIC_IRQChannelCmd=ENABLE;
	NVIC_Init(&NVIC_initestruct1);
	
}


int gTimer=0;
char g[20];
void TIM2_IRQHandler(void)
{
  if(TIM_GetITStatus(TIM2,TIM_IT_Update)!=RESET)//??????
  {	
    TIM_ClearITPendingBit(TIM2,TIM_IT_Update);//??????
    gTimer++;
	// USART_putString("你们好啊");
		//USART_Putc('s');
		//	USART_putInt(10);
		sprintf(g," %d",gTimer);
		//printf("a");
		USART_putString(g);
  }
}


void USART2_IRQHandler(void)    
{   

	uint16_t ch;
	uint8_t i=0;
    if(USART_GetITStatus(USART2,USART_IT_RXNE) != RESET) //????  
    {    
		//	USART_Putc('q');
	
     USART_ClearITPendingBit(USART2, USART_IT_RXNE);
			
		  ch = USART_ReceiveData(USART2);     //????
      USART_Putc(ch);
			 
			
		}
	//	if(USART_GetITStatus(USART2, USART_IT_TXE) != RESET){  
       // 
         //  USART_ITConfig(USART1, USART_IT_TXE, DISABLE);   
       //  }
  
}  
    


