# Itentika .NET Test Project
## Contents
+ [Description](#description)
  + [Event entity example](#event-entity-example)
  + [Incident entity example](#incident-entity-example)
  + [Event processing](#event-processing)
  + [Technology stack](#technology-stack)
+ [Logging](#logging)
  + [Event Publisher](#event-publisher)
  + [Event Processor](#event-processor)
+ [Building and Deployment](#building-and-deployment)
  + [Event Publisher configuration](#event-publisher-configuration)
  + [Event Processor configuration](#event-processor-configuration)
  + [Docker environment](#docker-environment)
## Description
Itentika .NET Test Project is simple microservice architecture web API. It\`s job is to generate events and process them as incidents.
### Event entity example
``` json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "type": 1,
  "time": "2023-11-29T19:50:19.654Z"
}
```
`Event Type` can take values `1`, `2`, `3` or `4`
### Incident entity example
```json
{
    "events": [
      {
        "id": "1ac091df-5dea-496d-aea6-53cfba282e0f",
        "type": 1,
        "time": "2023-11-29T19:21:04.287445Z"
      },
      {
        "id": "33ac8ca6-21c4-42ea-ac51-669f17b18820",
        "type": 2,
        "time": "2023-11-29T19:21:04.010978Z"
      }
    ],
    "id": "56e4e4fa-45fe-44ae-a62d-1e640df53024",
    "type": 2,
    "time": "2023-11-29T19:21:04.032218Z"
  }
```
`Incident Type` can take values `1` or `2`
### Event processing
1. If we receive `event` of type `1` we process it as `incident` of type `1`
2. If we receive `event` of type `2` and then that we receive `event` of type `1`  within 20 seconds than we process this 2 `events` as `incident` of type `2`, else we process `event` of type `1` as `incident` of type `1`

### Technology stack
APS NET CORE (NET6), Entity Framework Core, PostgreSQL, Docker compose
## Logging
Services use `Serilog` to log information
### Event Publisher
`Event Publisher` service logs information to `information level` about every attempt to send generated event to `Event Processor` (to `error level` if attempt failed):
```bash
2023-11-29 22:24:58 [19:24:58:419 INF ()] Send event {"Id": "4db55510-b2cd-47c5-9a8b-6729836b1126", "Type": "Type4", "Time": "2023-11-29T19:24:58.4195619Z", "$type": "Event"} to http://itentikatest_eventprocessor:80/api/EventProcessor/add
2023-11-29 22:24:58 [19:24:58:419 INF ()] Start processing HTTP request POST http://itentikatest_eventprocessor/api/EventProcessor/add
2023-11-29 22:24:58 [19:24:58:419 INF ()] Sending HTTP request POST http://itentikatest_eventprocessor/api/EventProcessor/add
2023-11-29 22:24:58 [19:24:58:420 INF ()] Received HTTP response headers after 0.785ms - 200
2023-11-29 22:24:58 [19:24:58:420 INF ()] End processing HTTP request after 0.9038ms - 200
2023-11-29 22:24:58 [19:24:58:972 INF ()] Send event {"Id": "a81f14b5-7566-43bf-8cc5-9b77d4b80e7d", "Type": "Type4", "Time": "2023-11-29T19:24:58.9729436Z", "$type": "Event"} to http://itentikatest_eventprocessor:80/api/EventProcessor/add
2023-11-29 22:24:58 [19:24:58:973 INF ()] Start processing HTTP request POST http://itentikatest_eventprocessor/api/EventProcessor/add
2023-11-29 22:24:58 [19:24:58:973 INF ()] Sending HTTP request POST http://itentikatest_eventprocessor/api/EventProcessor/add
2023-11-29 22:24:58 [19:24:58:973 INF ()] Received HTTP response headers after 0.6408ms - 200
2023-11-29 22:24:58 [19:24:58:973 INF ()] End processing HTTP request after 0.729ms - 200
```
It also logs information about every generated event to `trace level`.
### Event Processor
`Event Processor` logs information to `information level` about every processed event and created incident:
```bash
2023-11-29 22:25:00 [19:25:00:900 INF ()] Processing event {"Id": "4dd49926-a8a0-4327-b853-bbff43c72e56", "Type": "Type2", "Time": "2023-11-29T19:25:00.8834209Z", "$type": "Event"}
2023-11-29 22:25:02 [19:25:02:370 INF ()] Processing event {"Id": "4a69b9e9-64d7-4d73-bc80-d09194ce5727", "Type": "Type1", "Time": "2023-11-29T19:25:02.3601562Z", "$type": "Event"}
2023-11-29 22:25:02 [19:25:02:382 INF ()] Incident {"Id": "0c0ef1dd-6db9-4096-a33d-43e0dfe9d466", "Type": "Type2", "Time": "2023-11-29T19:25:00.9003476Z", "Events": [{"Id": "4dd49926-a8a0-4327-b853-bbff43c72e56", "Type": "Type2", "Time": "2023-11-29T19:25:00.8834209Z", "$type": "Event"}, {"Id": "4a69b9e9-64d7-4d73-bc80-d09194ce5727", "Type": "Type1", "Time": "2023-11-29T19:25:02.3601562Z", "$type": "Event"}], "$type": "Incident"} created
```

## Building and Deployment
You can build and deploy this project on your `local machine` or use [`Docker compose`](docker-compose.yml).
### Event Publisher configuration
Change configuration in [`appsettings.json`](Systems/ITentikaTest.EventPublisher/appsettings.json) or override it in [`appsettings.Development.json`](Systems/ITentikaTest.EventPublisher/appsettings.Development.json) file:
```json
{
  "EventGenerator": {
    "MaxDelay": 2000
  },
  "EventProcessor": {
    "Uri": "http://localhost:5167/api/EventProcessor/add",
    "Name": "EventProcessor"
  }
}
```
Change `EventGenerator.MaxDelay` to speed up or slow down even generation (delay is random in [0, `MaxDelay`]).
Change `EventProcessor.Uri` to set `receiving event` endpoint route.

### Event Processor configuration
Change configuration in [`appsettings.json`](Systems/ITentikaTest.EventProcessor/appsettings.json) or override it in [`appsettings.Development.json`](Systems/ITentikaTest.EventProcessor/appsettings.Development.json) file:
```json
{
  "EventProcessorDbContext": {
    "Type": "PostgreSQL",
    "ConnectionString": "Server=localhost;Port=5432;Database=ITentikaTest;User Id=postgres;Password=Passw0rd;"
  }
}
```
Set database configuration for db context (in this case `EventProcessorDbContext`)
### Docker environment
Change [`env.publisher`](env.publisher) to configure containerized `Event Publisher`
```
EventProcessor__Uri=http://itentikatest_eventprocessor:80/api/EventProcessor/add
EventProcessor__Name=EventProcessor
```
Change [`env.processor`](env.processor) to configure containerized `Event Processor`
```
EventProcessorDbContext__Type=PostgreSQL
EventProcessorDbContext__ConnectionString=Server=itentikatest_postgres;Port=5432;Database=ITentikaTest;User Id=postgres;Password=Passw0rd;
```