#include "fsmc.h"

void FSMC_init(void)
{
	FSMC_NORSRAMInitTypeDef  FSMC_NORSRAMInitStructure;
  FSMC_NORSRAMTimingInitTypeDef  p;	    
  //ʹ��FSMC����ʱ��
  RCC_AHBPeriphClockCmd(RCC_AHBPeriph_FSMC, ENABLE); 
	
	 //FSMC�ӿ���������
  p.FSMC_AddressSetupTime = 0x02;//���õ�ַ����ʱ��
  p.FSMC_AddressHoldTime = 0x00;//���õ�ַ����ʱ��
  p.FSMC_DataSetupTime = 0x05;//�������ݽ���ʱ��
  p.FSMC_BusTurnAroundDuration = 0x00;//�������߷�תʱ��
  p.FSMC_CLKDivision = 0x00;//ʱ�ӷ�Ƶ
  p.FSMC_DataLatency = 0x00;//���ݱ���ʱ��
  p.FSMC_AccessMode = FSMC_AccessMode_B;//����FSMC����ģʽ

 
  FSMC_NORSRAMInitStructure.FSMC_Bank = FSMC_Bank1_NORSRAM1; //ѡ������bank�Լ�Ƭѡ�ź�
  FSMC_NORSRAMInitStructure.FSMC_DataAddressMux = FSMC_DataAddressMux_Disable;//�����Ƿ���������ʱ�ָ���
  FSMC_NORSRAMInitStructure.FSMC_MemoryType = FSMC_MemoryType_NOR;//���ô洢������
  FSMC_NORSRAMInitStructure.FSMC_MemoryDataWidth = FSMC_MemoryDataWidth_16b;//�������ݿ��
  FSMC_NORSRAMInitStructure.FSMC_BurstAccessMode = FSMC_BurstAccessMode_Disable;//�����Ƿ�������дģʽ
  FSMC_NORSRAMInitStructure.FSMC_WaitSignalPolarity = FSMC_WaitSignalPolarity_Low;//����wait�źŵ���Ч��ƽ
  FSMC_NORSRAMInitStructure.FSMC_WrapMode = FSMC_WrapMode_Disable;//�����Ƿ�ʹ�û���ģʽ
  FSMC_NORSRAMInitStructure.FSMC_WaitSignalActive = FSMC_WaitSignalActive_BeforeWaitState;//����wait�źŵ���Чʱ��
  FSMC_NORSRAMInitStructure.FSMC_WriteOperation = FSMC_WriteOperation_Enable;//�����Ƿ�ʹ��д����
  FSMC_NORSRAMInitStructure.FSMC_WaitSignal = FSMC_WaitSignal_Disable;//�����Ƿ�ʹ��wait�ź�
  FSMC_NORSRAMInitStructure.FSMC_ExtendedMode = FSMC_ExtendedMode_Disable;//�����Ƿ���չģʽ
  FSMC_NORSRAMInitStructure.FSMC_WriteBurst = FSMC_WriteBurst_Disable;//�����Ƿ�����дģʽ
  FSMC_NORSRAMInitStructure.FSMC_ReadWriteTimingStruct = &p;//���ö�дʱ��
  FSMC_NORSRAMInitStructure.FSMC_WriteTimingStruct = &p;//����дʱ��
 
  FSMC_NORSRAMInit(&FSMC_NORSRAMInitStructure); 		
  /* Enable FSMC Bank1_SRAM Bank */
  FSMC_NORSRAMCmd(FSMC_Bank1_NORSRAM1, ENABLE);  
}


