using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace LockerClient.Test
{
    public class CoderTest
    {
        [Theory]
        [InlineData("dfghfdghfdgh", "ZGZnaGZkZ2hmZGdo")]
        [InlineData("gfhjdfjgdfjdfnj ffhdsafsghjmfghnjmjhhgfgh", "Z2ZoamRmamdkZmpkZm5qIGZmaGRzYWZzZ2hqbWZnaG5qbWpoaGdmZ2g=")]
        [InlineData("demofgfdgdgfd", "ZGVtb2ZnZmRnZGdmZA==")]
        [InlineData("dd", "ZGQ=")]
        public void Encoding_Test(string plainText, string encodedText)
        {
            ICoder coder = new Coder();

            var encoded = coder.Encode(Encoding.UTF8.GetBytes(plainText));

            encoded.Should().Be(encodedText);
        }

        [Theory]
        [InlineData("dfghfdghfdgh", "ZGZnaGZkZ2hmZGdo")]
        [InlineData("gfhjdfjgdfjdfnj ffhdsafsghjmfghnjmjhhgfgh", "Z2ZoamRmamdkZmpkZm5qIGZmaGRzYWZzZ2hqbWZnaG5qbWpoaGdmZ2g=")]
        [InlineData("demofgfdgdgfd", "ZGVtb2ZnZmRnZGdmZA==")]
        [InlineData("dd", "ZGQ=")]
        public void Decoding_Test(string plainText, string encodedText)
        {
            ICoder coder = new Coder();

            var decoded = coder.Decode(encodedText);

            decoded.Should().BeEquivalentTo(Encoding.UTF8.GetBytes(plainText));
        }
    }
}
