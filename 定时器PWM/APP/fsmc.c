#include "fsmc.h"

void FSMC_init(void)
{
	FSMC_NORSRAMInitTypeDef  FSMC_NORSRAMInitStructure;
  FSMC_NORSRAMTimingInitTypeDef  p;	    
  //使能FSMC外设时钟
  RCC_AHBPeriphClockCmd(RCC_AHBPeriph_FSMC, ENABLE); 
	
	 //FSMC接口特性配置
  p.FSMC_AddressSetupTime = 0x02;//设置地址建立时间
  p.FSMC_AddressHoldTime = 0x00;//设置地址保存时间
  p.FSMC_DataSetupTime = 0x05;//设置数据建立时间
  p.FSMC_BusTurnAroundDuration = 0x00;//设置总线反转时间
  p.FSMC_CLKDivision = 0x00;//时钟分频
  p.FSMC_DataLatency = 0x00;//数据保存时间
  p.FSMC_AccessMode = FSMC_AccessMode_B;//设置FSMC访问模式

 
  FSMC_NORSRAMInitStructure.FSMC_Bank = FSMC_Bank1_NORSRAM1; //选择设置bank以及片选信号
  FSMC_NORSRAMInitStructure.FSMC_DataAddressMux = FSMC_DataAddressMux_Disable;//设置是否数据总线时分复用
  FSMC_NORSRAMInitStructure.FSMC_MemoryType = FSMC_MemoryType_NOR;//设置存储器类型
  FSMC_NORSRAMInitStructure.FSMC_MemoryDataWidth = FSMC_MemoryDataWidth_16b;//设置数据宽度
  FSMC_NORSRAMInitStructure.FSMC_BurstAccessMode = FSMC_BurstAccessMode_Disable;//设置是否连续读写模式
  FSMC_NORSRAMInitStructure.FSMC_WaitSignalPolarity = FSMC_WaitSignalPolarity_Low;//设置wait信号的有效电平
  FSMC_NORSRAMInitStructure.FSMC_WrapMode = FSMC_WrapMode_Disable;//设置是否使用还回模式
  FSMC_NORSRAMInitStructure.FSMC_WaitSignalActive = FSMC_WaitSignalActive_BeforeWaitState;//设置wait信号的有效时机
  FSMC_NORSRAMInitStructure.FSMC_WriteOperation = FSMC_WriteOperation_Enable;//设置是否使能写操作
  FSMC_NORSRAMInitStructure.FSMC_WaitSignal = FSMC_WaitSignal_Disable;//设置是否使用wait信号
  FSMC_NORSRAMInitStructure.FSMC_ExtendedMode = FSMC_ExtendedMode_Disable;//设置是否扩展模式
  FSMC_NORSRAMInitStructure.FSMC_WriteBurst = FSMC_WriteBurst_Disable;//设置是否连续写模式
  FSMC_NORSRAMInitStructure.FSMC_ReadWriteTimingStruct = &p;//设置读写时序
  FSMC_NORSRAMInitStructure.FSMC_WriteTimingStruct = &p;//设置写时序
 
  FSMC_NORSRAMInit(&FSMC_NORSRAMInitStructure); 		
  /* Enable FSMC Bank1_SRAM Bank */
  FSMC_NORSRAMCmd(FSMC_Bank1_NORSRAM1, ENABLE);  
}


