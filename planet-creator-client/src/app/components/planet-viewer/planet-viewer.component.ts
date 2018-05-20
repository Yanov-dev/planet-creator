import {AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';

import * as THREE from 'three';
import * as OBJLoader from 'three-obj-loader';
import * as MTLLoader from 'three-mtl-loader';

@Component({
  selector: 'app-planet-viewer',
  templateUrl: './planet-viewer.component.html'
})
export class PlanetViewerComponent implements AfterViewInit {
  private camera: THREE.PerspectiveCamera;

  private get canvas(): HTMLCanvasElement {
    return this.canvasRef.nativeElement;
  }

  @ViewChild('canvas')
  private canvasRef: ElementRef;

  private cube: any;

  private renderer: THREE.WebGLRenderer;

  private scene: THREE.Scene;

  @Input()
  public cameraZ: number = 5;

  @Input()
  public fieldOfView: number = 70;

  @Input('nearClipping')
  public nearClippingPane: number = 1;

  @Input('farClipping')
  public farClippingPane: number = 1000;

  constructor() { }

  private animateCube() {
    if (!this.cube)
      return;
    this.cube.rotation.x += 0.005;
    this.cube.rotation.y += 0.01;
  }

  private createCube() {
    OBJLoader(THREE);

    var localScene = this.scene;
    var mtlLoader = new MTLLoader();

    let texture = new THREE.TextureLoader().load('/assets/image.png');
    let material = new THREE.MeshBasicMaterial({ map: texture });
    //material = new THREE.MeshBasicMaterial( { color: 0xff0000, wireframe: true } );
    var tThis = this;
    var objLoader = new THREE.OBJLoader();
    objLoader.load('/assets/planet.obj', function (object) {
      object.traverse(function (child) {
        if (child instanceof THREE.Mesh) {
          child.material = material;
        }
      });
      localScene.add(object);
      tThis.cube = object;
    });
  }

  private createScene() {
    this.scene = new THREE.Scene();

    let aspectRatio = this.getAspectRatio();
    this.camera = new THREE.PerspectiveCamera(
      this.fieldOfView,
      aspectRatio,
      this.nearClippingPane,
      this.farClippingPane
    );
    this.camera.position.z = this.cameraZ;
  }

  private getAspectRatio() {
    return this.canvas.clientWidth / this.canvas.clientHeight;
  }

  private startRenderingLoop() {
    this.renderer = new THREE.WebGLRenderer({ canvas: this.canvas });
    this.renderer.setPixelRatio(devicePixelRatio);
    this.renderer.setSize(this.canvas.clientWidth, this.canvas.clientHeight);

    let component: PlanetViewerComponent = this;
    (function render() {
      requestAnimationFrame(render);
      component.animateCube();
      component.renderer.render(component.scene, component.camera);
    }());
  }

  public onResize() {
    this.camera.aspect = this.getAspectRatio();
    this.camera.updateProjectionMatrix();

    this.renderer.setSize(this.canvas.clientWidth, this.canvas.clientHeight);
  }

  public ngAfterViewInit() {
    this.createScene();
    this.createCube();
    this.startRenderingLoop();
  }

}
