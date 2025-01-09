API handling web request in golang

packages : https package installation

net/http most popular recommended


response :
    type res struct{ status, status code}
    return response everytime

    header can be chngd nd checked
    readresponse nor response.write ever close connection - so make sure u close collection 


get request:

