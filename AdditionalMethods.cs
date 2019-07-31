using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Youtube.NET
{
    public class AdditionalMethods
    {
        public class StringTransformation
        {
            public enum TransformationMethod
            {
                Replace,
                Remove,
                Append,
                Prepend
            }

            public TransformationMethod Method { get; set; }

            public bool IgnoreCase { get; set; }

            /// <summary>
            /// Indicates the value that is being appended, prepended, replaced or removed
            /// </summary>
            public string PrimaryValue { get; set; }
            /// <summary>
            /// Indicates value used for replace methods only (others are ignored)
            /// </summary>
            public string SecondaryValue { get; set; } = null;
        }

        public static (bool, int, int, string) DoReplacements(string description, StringTransformation[] transformations)
        {
            int replacementSum = 0;
            int removalSum = 0;
            bool updated = false;
            foreach (var transformation in transformations)
            {
                var result = DoReplacements(description, transformation);
                if (result.Item1 == StringTransformation.TransformationMethod.Replace)
                {
                    replacementSum += result.Item3;
                }
                else if (result.Item1 == StringTransformation.TransformationMethod.Remove)
                {
                    removalSum += result.Item3;
                }

                updated = true;
                description = result.Item4;
            }

            return (updated, replacementSum, removalSum, description);
        }

        public static (StringTransformation.TransformationMethod, bool, int, string, string) DoReplacements(string original, StringTransformation transformation)
        {
            var updatedDescription = new string(original.ToCharArray());
            int amount = 0;
            switch (transformation.Method)
            {
                case StringTransformation.TransformationMethod.Replace:
                    var replaceResult = updatedDescription.Replace(transformation.PrimaryValue, transformation.SecondaryValue, transformation.IgnoreCase);
                    amount = replaceResult.Item1;
                    updatedDescription = replaceResult.Item2;
                    break;
                case StringTransformation.TransformationMethod.Remove:
                    var removeResult = updatedDescription.Remove(transformation.PrimaryValue, transformation.IgnoreCase);
                    amount = removeResult.Item1;
                    updatedDescription = removeResult.Item2;
                    break;
                case StringTransformation.TransformationMethod.Append:
                    updatedDescription = updatedDescription.Append(transformation.PrimaryValue);
                    break;
                case StringTransformation.TransformationMethod.Prepend:
                    updatedDescription = updatedDescription.Prepend(transformation.PrimaryValue);
                    break;
            }

            
            return (transformation.Method, !updatedDescription.Equals(original), amount, updatedDescription, original);
        }

        public async Task ReplaceDescriptions(Uploads uploadService, Video[] videos, StringTransformation[] transformations)
        {
            foreach (var video in videos)
            {
                var description = video.Snippet.Description;

                foreach (var transformation in transformations)
                {

                }

                await uploadService.UpdateDescription(video, description);
            }
        }
    }
}
