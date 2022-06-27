using Confluent.Kafka;
using DockerContainer_Kafka_Demo;
using Newtonsoft.Json;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine("Write anything");
Console.WriteLine("Digit: ");
Console.ReadLine();


var ProducerConfig = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<Null,string>(ProducerConfig).Build();

try
{
    var response = await  producer.ProduceAsync("mytopic", 
        new Message<Null, string> { Value = JsonConvert.SerializeObject(
            new TopicModel { TopicName="Topic1", TopicDescription = "An example of item in kafka queue" }
               )});

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


Console.ReadLine();
var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092"
                    ,
    GroupId = "mytopic-consumer-group"
                    ,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();

consumer.Subscribe("mytopic");

CancellationTokenSource token = new();


try
{
    while (true)
    {
        var response = consumer.Consume(token.Token);
        if(response.Message != null)
        {
            var topic = JsonConvert.DeserializeObject<TopicModel>(response.Message.Value);
            Console.WriteLine($"TopicName: {topic.TopicName}, TopicDescription: {topic.TopicDescription}");
        }
    }
}   
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());

}

Console.ReadLine();
