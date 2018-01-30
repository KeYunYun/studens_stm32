#include"sys.h"
#include"usart.h"
#include"daley.h"
#include"stm32f10x.h"
int main(void)
{
	Stm32_Clock_Init(9);
	uart_init(72,9600);


}
