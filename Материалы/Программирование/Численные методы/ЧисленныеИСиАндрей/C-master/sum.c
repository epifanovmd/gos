#include<stdio.h>
#include<stdlib.h>

  int main(void)
  {
  int n, sum = 0;
     printf("Vvedite kol elements: ");
     scanf("%d", &n);
     int * mass = malloc(n * sizeof(int));

     for(int i = 0; i < n; i++)
     {
	mass[i] = 0 + random()%20;
	sum += mass[i];
	printf("%d ", mass[i]);
     }
     printf("\nsum = %d\n", sum);

     return 0;
     
  }
