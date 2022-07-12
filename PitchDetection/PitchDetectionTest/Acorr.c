#include <stdio.h>

// #include "AltoSaxVib_C4.h"
// #include "Piano_C4.h"
//#include "Guitar_C5.h"
#include "C4.h"

// Sample Frequency in Hz
const float sample_freq = 22050;

int len = sizeof(rawData);


void main() {
    float sum = 0.0, sum_old;
    float thresh = 0;
    float freq_per = 0;
    int pd_state = 0;
    int period = 0;
  
//   for( int i = 0; i < len; i++ ) {
//       fprintf( stdout, "%g %g\n", (float)i/sample_freq, (float)(rawData[i] - 128) / 128.0 );
//   }
    printf("%d", len);
    for( int i = 0; i < len; i++ ) {
        // Autocorrelation
       
        sum_old = sum;
        sum = 0.0;
        for( int k = 0; k < len-i; k++ ) {
            sum += 0.000061035 * (float)((rawData[k]-128) * (rawData[k+i]-128));
        }
//         fprintf( stdout, "%g %g\n", (float)i/sample_freq, sum );
    
        // Peak Detect State Machine
        if (pd_state == 2 && (sum-sum_old) <=0) {
            period = i-1;
            pd_state = 3;
        }
        
        if (pd_state == 1 && (sum > thresh) && (sum-sum_old) > 0) pd_state = 2;
        if (!i) {
            
            
            thresh = sum * 0.5;
            pd_state = 1;
        }
    }
    
    // Frequency identified in Hz
    freq_per = sample_freq/period;
    fprintf( stderr, "f = %g\n", freq_per );
}
