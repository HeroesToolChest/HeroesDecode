# Dagger CICD

Building the docker image and passing a replay file from the host system into the dagger engine

## building and using the heroes decode CLI directly with dagger

```sh
dagger call build --dir=.\HeroesDecode with-file --source=C://Projects/HeroesDecode/VolskayaFoundry.StormReplay --path=/input/replay.StormReplay with-exec --args=./HeroesDecode,--replay-file,/input/replay.StormReplay stdout
```

## Building the docker image and exporting the .tar.gz image to the local file system

### Build the image and export it

```sh
dagger call build --dir=.\HeroesDecode as-tarball export ./image/HeroesDecode.tar.gz
```

### Load the image into Docker

```sh
docker image -i load ./HeroesDecode.tar.gz
```

### Tag the image

```sh
docker image tag [IMAGE ID] heroes-decode:latest
```

## Use the docker image

### stdout for replay file

```sh
docker run -v .\VolskayaFoundry.StormReplay:/input/replay.StormReplay heroes-decode:latest --replay-path /input/replay.StormReplay
```

### parse and output to host system

```sh
docker run -v .\output:/output/ -v .\VolskayaFoundry.StormReplay:/input/replay.StormReplay heroes-decode:latest --replay-path /input/replay.StormReplay get-json -o /output
```
