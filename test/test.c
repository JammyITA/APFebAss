#include <stdlib.h>

typedef int bool;
#define true 1
#define false 0

int main(){
	bool a = true;
	char input[] = "Let a = true";


	bool result = a;

	printf("Evaluating:\n\t %s \n Result: %s\t", input, result ? "true" : "false");

	return 0;
}