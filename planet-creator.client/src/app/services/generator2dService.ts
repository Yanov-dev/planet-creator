import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ShemaLayer } from "../models/shemaLayer";

@Injectable()
export class Generator2dService {

    private obj: any = {
        "area": {
            "fullWidth": 200,
            "fullHeight": 200,
            "Rect": {
                "x": 0,
                "y": 0,
                "width": 200,
                "height": 200
            }
        },
        "shema": {
            "Layers": [
                {
                    "Colors": [
                        {
                            "Level": 0,
                            "Color": "#FF000000"
                        },
                        {
                            "Level": 640,
                            "Color": "#FF483D8B"
                        },
                        {
                            "Level": 675,
                            "Color": "#FF8470FF"
                        },
                        {
                            "Level": 770,
                            "Color": "#FFEEDD82"
                        },
                        {
                            "Level": 945,
                            "Color": "#FF6B8E23"
                        },
                        {
                            "Level": 1275,
                            "Color": "#FF000000"
                        }
                    ],
                    "IsEnable": false,
                    "Seed": 0.0
                }
            ],
            "Lng": 0.0,
            "Lat": 0.0,
            "SeaLevel": 0.0,
            "ProjectionType": 0
        }
    };

    constructor(private _http: HttpClient) {
    }

    Generate(): Observable<any> {
        return this._http.post(environment.links.generate2d, this.obj);
    }

    GenerateLayerPreview(layer: ShemaLayer): Observable<any> {
        return this._http.post(environment.links.layerPreview, layer);
    }
}