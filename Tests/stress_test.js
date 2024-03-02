import http from 'k6/http';
import { sleep } from 'k6';
export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '2m', target: 100 }, // below normal load ( target = 100 users, for duration of 2 minutes)
        { duration: '5m', target: 100 }, // 
        { duration: '2m', target: 200 }, // normal load
        { duration: '5m', target: 200 }, // 
        { duration: '2m', target: 300 }, // aorund the breaking point
        { duration: '5m', target: 300 }, // 
        { duration: '2m', target: 400 }, // beyong breaking point
        { duration: '5m', target: 400 }, // 
        { duration: '10m', target: 0 }, // scale down, recovery stage
    ]
};

export default () => {
    http.get('http://localhost:8080/api/v1/weather/london');
};