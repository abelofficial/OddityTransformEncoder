## Encoding using Oddity encoder

**Step 1**: Count how many times each byte occurred in the data and map them to the byte in an X by Y map, where X \* Y = 256 (Since we are using 8-bits for this example).

![Oddity byte count map](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/tv9erzsm50c53g1xvphs.png)

**Step 2**: Sort the map based on the appearance count. The first 32 bytes with the highest appearance count will be listed in the first column (House 0), the next 32 bytes on House 1, and so on. If two or more bytes with the same appearance count exist, the highest byte always comes first. **This Map will be used as our encoding map**.

![Oddity encoding map](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/3i0fimyxllbptfglnct6.png)

**Step 3**: For each byte in the data.

- Locate the byte on the encoder map.

- Save the X and Y axis for the byte separately.

Eg: Byte value 217: X = 0, and Y = 4 (From the map above.)

**NOTE**: both X and Y should use the minimum amount of bits, meaning since the possible values for the X-axis are 0 - 7, 3 bits are enough. For the Y-axis possible values are 0 - 31, therefore 5-bits are enough. By doing this, we will make sure that we are not increasing the size of the encoded data from the original data.

### The encoded data

By the end of step 3, we will have 3 sets of data which are the appearance count(from step 1), a list of Y axis, and a list of X-axis (from step 3). The size of the Y-axis list plus the X-axis list MUST be equal to the original data size. Moreover, for each X-axis value, there must be One Y-axis value.

The appearance count list is a set of 256 bytes representing the appearance count for each byte from 0 - 256 in order. The appearance count list must be included at the beginning of the encoded data for the Decoder to successfully reformulate the encoding map. The following image shows the encoded data structure.

![Encoded data structure](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/8wrvapfgmmdiovquip7i.png)

## Decoding using Oddity

1. Extract the header, X-axis, and Y-axis from encoded data.

   **Note**: _The header is the first 256 values. The remaining data represents the encoded data of the X-axises and Y-axises. For each 5-bit of Y-axis, there is a 3-bit X-axis value_.

2. Create a Map for the appearance count with the byte it represents(the index of each appearance count represents the byte). [Example for appearance count map](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/tv9erzsm50c53g1xvphs.png)

3. Sort the Map to get the Encoding map.
   **Note**: _The same as the encoding process, if two or more bytes with the same appearance count exist, the highest byte always comes first_. [Example for encoding map](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/3i0fimyxllbptfglnct6.png)

4. For each X-axis value x and Y-axis value y.

- Locate the byte on encoder map(x,y): The located byte will represent the original byte before encoding.

As alway with 💜
