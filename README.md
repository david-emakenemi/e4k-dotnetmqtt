# Dotnet MQTT Sample

A small Mqttnet sample to connect and publish messages to the E4K broker

## Building and deploying

To build image:

```sh
docker build -t myimagename -f Dockerfile .
```

Import your image into your E4k cluster/push your image to your respective repo
Once E4K is running, apply your yaml:

```sh
kubectl apply -f ./pod.yaml
```
