/* 
 * Workspace API
 *
 * Agent API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Genesys.Workspace.Client.SwaggerDateConverter;

namespace Genesys.Workspace.Model
{
    /// <summary>
    /// InlineResponse2001Data
    /// </summary>
    [DataContract]
    public partial class InlineResponse2001Data :  IEquatable<InlineResponse2001Data>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineResponse2001Data" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected InlineResponse2001Data() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineResponse2001Data" /> class.
        /// </summary>
        /// <param name="SubscriptionId">ID used to fetch statistics values from /reporting/{subscriptionId} (required).</param>
        /// <param name="Statistics">The list of all the registered statitstics. (required).</param>
        public InlineResponse2001Data(string SubscriptionId = default(string), List<StatisticValueForRegisterResponse> Statistics = default(List<StatisticValueForRegisterResponse>))
        {
            // to ensure "SubscriptionId" is required (not null)
            if (SubscriptionId == null)
            {
                throw new InvalidDataException("SubscriptionId is a required property for InlineResponse2001Data and cannot be null");
            }
            else
            {
                this.SubscriptionId = SubscriptionId;
            }
            // to ensure "Statistics" is required (not null)
            if (Statistics == null)
            {
                throw new InvalidDataException("Statistics is a required property for InlineResponse2001Data and cannot be null");
            }
            else
            {
                this.Statistics = Statistics;
            }
        }
        
        /// <summary>
        /// ID used to fetch statistics values from /reporting/{subscriptionId}
        /// </summary>
        /// <value>ID used to fetch statistics values from /reporting/{subscriptionId}</value>
        [DataMember(Name="subscriptionId", EmitDefaultValue=false)]
        public string SubscriptionId { get; set; }

        /// <summary>
        /// The list of all the registered statitstics.
        /// </summary>
        /// <value>The list of all the registered statitstics.</value>
        [DataMember(Name="statistics", EmitDefaultValue=false)]
        public List<StatisticValueForRegisterResponse> Statistics { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineResponse2001Data {\n");
            sb.Append("  SubscriptionId: ").Append(SubscriptionId).Append("\n");
            sb.Append("  Statistics: ").Append(Statistics).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as InlineResponse2001Data);
        }

        /// <summary>
        /// Returns true if InlineResponse2001Data instances are equal
        /// </summary>
        /// <param name="other">Instance of InlineResponse2001Data to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineResponse2001Data other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.SubscriptionId == other.SubscriptionId ||
                    this.SubscriptionId != null &&
                    this.SubscriptionId.Equals(other.SubscriptionId)
                ) && 
                (
                    this.Statistics == other.Statistics ||
                    this.Statistics != null &&
                    this.Statistics.SequenceEqual(other.Statistics)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.SubscriptionId != null)
                    hash = hash * 59 + this.SubscriptionId.GetHashCode();
                if (this.Statistics != null)
                    hash = hash * 59 + this.Statistics.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
