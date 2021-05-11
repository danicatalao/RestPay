db = db.getSiblingDB('restPayDB');

db.createCollection('transacoes',function(error,collection) {
	collection.insert(docs, function(err, records) {});
});
