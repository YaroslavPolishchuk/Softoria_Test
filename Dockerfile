FROM postgres:latest

COPY PresetScript.sql /docker-entrypoint-initdb.d/

ENV POSTGRES_USER=admin
ENV POSTGRES_PASSWORD=secret
ENV POSTGRES_DB=news