FROM postgres

EXPOSE 5432

ENV POSTGRES_PASSWORD=password

CMD ["postgres"]
