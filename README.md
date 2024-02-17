# .NET API with OpenTelemetry and Grafana dashboards Proof of concept

TODO: Clean this up, including the links, and make it pretty :)

I heard about OpenTelemetry and Grafana through a recent blog post with .NET 8
https://devblogs.microsoft.com/dotnet/introducing-aspnetcore-metrics-and-grafana-dashboards-in-dotnet-8/

The .NET team added a lot more built in metrics for Web APIs starting with .NET 8. I was curious to try it out. However the documentation for setting this up was spotty at best. I saw some youtube videos but they didn't go into how to setup Grafana and Prometheus for scraping the data from the API via docker.

I followed this tutorial for Grafana and Prometheus in Docker:
https://grafana.com/docs/grafana-cloud/send-data/metrics/metrics-prometheus/prometheus-config-examples/docker-compose-linux/
however to get it to work with my .NET toy API, it required a LOT of changes (and hair pulling). Below are the steps I learned to get it all working.

To run this project, simply run the command 'docker compose up' and then go to localhost:3000 for Grafana, and send some API requests to localhost:8080/api/v1/weather/{city}?days={number}

## Steps I had to take to get it working

- Ensure that the api container, as well as the node-exporter, grafana and prometheus containers are all on the same network (in the docker-compose file, they are under the network 'monitoring'). This ensures that each container can talk to one enough in docker.

- Ensure the prometheus ports are exposed so you can access it locally (it's running in docker, and will be accessible to the other containers, but if you want to use the UI on your machine, you need to expose the port via 'ports: - "9090:9090"')

- In the prometheus.yml file, make sure for each of the scrape_configs, that the 'targets' section uses the name of the container defined in the docker-compose file, and NOT localhost. i.e. prometheus:9090 rather than localhost:9090. This is because services running in an internal docker network cannot use localhost to reach one another.

- When in the grafana UI (accessed on your machine through localhost:3000), you need to setup a data source connection for prometheus, make sure the 'Connection Server URL' is set to 'http://prometheus:9090', for the same reason described above.

Ports used:

- 9090 for prometheus
- 9100 for node-exporter
- 3000 for grafana
- 8080 for the API (including in the Dockerfile EXPOSES statement)
