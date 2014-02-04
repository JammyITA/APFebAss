#include <stdlib.h>

typedef int bool;
#define true 1
#define false 0

int main(){
	bool a = true;

	bool result = true;
	char input[] = "Let a = true";


	{
        bool a = false;		
        bool c = true;

		result = result && a && c;
	}

	printf("Evaluating:\n\t %s \nResult: %s\t", input, result ? "true" : "false");

	return 0;
}