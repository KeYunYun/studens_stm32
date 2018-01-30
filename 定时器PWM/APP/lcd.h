#ifndef __LCD_H
#define __LCD_H
#include "stm32f10x.h"


#define Bank1_LCD_D    ((uint32_t)0x60020000)    //显示区数据地址	  
#define Bank1_LCD_C    ((uint32_t)0x60000000)	 //显示区指令地址

void LCD_Init(void);
void LCD_WR_REG(unsigned int index);
void LCD_WR_CMD(unsigned int index,unsigned int val);
void LCD_WR_Data(unsigned int val);
void LCD_test(void);
void lcd_DrawPicture(u16 StartX,u16 StartY,u8 Dir,u8 *pic);
void lcd_wr_zf(u16 StartX, u16 StartY, u16 X, u16 Y, u16 Color, u8 Dir, u8 *chr);
unsigned int LCD_RD_data(void);
unsigned char *num_pub(unsigned  int a);


#endif


